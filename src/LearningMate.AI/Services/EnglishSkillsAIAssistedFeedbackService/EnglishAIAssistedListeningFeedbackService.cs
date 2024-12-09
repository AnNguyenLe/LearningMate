using System.Text.Json;
using FluentResults;
using LearningMate.AI.Extensions;
using LearningMate.Core.DTOs.ListeningTopicDTOs;
using LearningMate.Core.ErrorMessages;
using LearningMate.Core.Errors;
using LearningMate.Core.LoggingMessages;
using Microsoft.Extensions.Logging;

namespace LearningMate.AI.Services.EnglishSkillsAIAssistedFeedbackService;

public partial class EnglishSkillsAIAssistedFeedbackService
{
    public async Task<Result<List<ListeningTopicQuestionFeedback>>> GenerateListeningFeedback(
        Guid topicId,
        ListeningTopicSubmitRequestDto submittedAnswer
    )
    {
        var topicSolutionQuery = await _listeningTopicsService.GetTopicSolutionAsync(topicId);
        var topicSolution = topicSolutionQuery.ValueOrDefault;

        if (topicSolutionQuery.IsFailed || topicSolution is null)
        {
            _logger.LogWarning(
                CommonLoggingMessages.FailedToPerformActionWithId,
                "get listening topic solution",
                topicId
            );
            return new ProblemDetailsError(
                CommonErrorMessages.FailedTo($"get listening topic solution with ID: {topicId}")
            );
        }

        var prompt = GeneratePromptForListeningTopicFeedback(submittedAnswer, topicSolution);

        var feedbackProcess = await _promptService.GenerateContent(prompt);

        if (feedbackProcess.IsFailed)
        {
            _logger.LogWarning(
                CommonLoggingMessages.FailedToPerformActionWithId,
                "generate reading topic feedback",
                topicId
            );
            return new ProblemDetailsError(
                CommonErrorMessages.FailedTo($"generate reading topic feedback with ID: {topicId}")
            );
        }

        var feedback = JsonSerializer.Deserialize<List<ListeningTopicQuestionFeedback>>(
            feedbackProcess.Value.RemoveJsonDelimiters()
        );

        return feedback!;
    }

    private static string GeneratePromptForListeningTopicFeedback(
        ListeningTopicSubmitRequestDto submittedAnswer,
        ListeningTopicSolutionResponseDto topicSolution
    )
    {
        var mergedQuestions = submittedAnswer
            .Questions!.Join(
                topicSolution.Questions!,
                examinee => examinee.Id,
                official => official.Id,
                (examinee, official) =>
                    new
                    {
                        official.Id,
                        official.Content,
                        AnswerOptions = examinee
                            .AnswerOptions!.Join(
                                official.AnswerOptions!,
                                ex => ex.Content,
                                officialAns => officialAns.Content,
                                (ex, officialAns) =>
                                    new
                                    {
                                        ex.Id,
                                        ex.Content,
                                        ex.IsSelected,
                                        officialAns.IsCorrectAnswer,
                                        Feedback = "fill-in-the-explanation-based-on-the-value-of-isCorrectAnswer-field-goes-here"
                                    }
                            )
                            .ToList()
                    }
            )
            .ToList();

        var json = JsonSerializer.Serialize(mergedQuestions);

        var prompt = $"""
                Based on this listening topic having:

                Title: {topicSolution.Title}
                Category: {topicSolution.Category}
                Content: {topicSolution.Content}
                
                Here is the questions and answer options accordingly in the JSON format:
                {json}
                where:
                isSelected's value represents examinee's answer
                IsCorrectAnswer's value represents correct answer from official solution.

                Write me back in JSON format and each item of the list using the following pattern so that I can easily destringify.
                Apply for all questions. And the final result should be in clean JSON format without json-denotation at the beginning:

                Question: <question-content>,
                SelectedAnswer: <examinee-selected-answer>,
                CorrectAnswer: <correct-answer-based-on-official-solution>,
                Explanation: <your-explanation-goes-here>
            """;

        return prompt;
    }
}
