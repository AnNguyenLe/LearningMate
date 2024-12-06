using System.Text.Json;
using FluentResults;
using LearningMate.AI.ServiceContracts.EnglishSkillsAIAssistedFeedbackServiceContract;
using LearningMate.AI.ServiceContracts.PromptServiceContract;
using LearningMate.Core.DTOs.ReadingTopicDTOs;
using LearningMate.Core.DTOs.ListeningTopicDTOs;
using LearningMate.Core.ErrorMessages;
using LearningMate.Core.Errors;
using LearningMate.Core.LoggingMessages;
using LearningMate.Core.ServiceContracts.ReadingTopicsServiceContract;
using LearningMate.Core.ServiceContracts.ListeningTopicsServiceContract;
using Microsoft.Extensions.Logging;

namespace LearningMate.AI.Services.EnglishSkillsAIAssistedFeedbackService;

// ---- ReadingTopicSubmitRequestDto ----
// {
//     id: "guid",
//     questions: [
//     {
//         id: "guid",
//         content: "How many countries are there in the world?",
//         answerOptions: [
//             {
//                 id: "guid",
//                 content: 195,
//                 isSelected: false
//             },
//             {
//                 id: "guid",
//                 content: 194,
//                 isSelected: true
//             },
//             {
//                 id: "guid",
//                 content: 193,
//                 isSelected: false
//             },
//             {
//                 id: "guid",
//                 content: 192,
//                 isSelected: false
//             }
//         ]
//     }
// ]
// }
// ----------------------------------------


// --- ReadingTopicSolutionResponseDto ---
// {
//     id: "guid",
//     category: "Geography",
//     title: "Countries of the world",
//     content: "Countries of the world Countries of the world Countries of the world Countries of the world",
//     questions: [
//          {
//              id: "guid",
//              content: "How many countries are there in the world?",
//              answerOptions: [
//                  {
//                      id: "guid",
//                      content: 195,
//                      IsCorrectAnswer: true
//                  },
//                  {
//                      id: "guid",
//                      content: 194,
//                      IsCorrectAnswer: false
//                  },
//                  {
//                      id: "guid",
//                      content: 193,
//                      IsCorrectAnswer: false
//                  },
//                  {
//                      id: "guid",
//                      content: 192,
//                      IsCorrectAnswer: false
//                  }
//               ]
//           }
//      ]
// }
// -------------------------------------------



public class EnglishSkillsAIAssistedFeedbackService(
    ILogger<EnglishSkillsAIAssistedFeedbackService> logger,
    IReadingTopicsService readingTopicsService,
    IListeningTopicsService listeningTopicsService,
    IPromptService promptService
) : IEnglishSkillsAIAssistedFeedbackService
{
    private readonly ILogger<EnglishSkillsAIAssistedFeedbackService> _logger = logger;
    private readonly IReadingTopicsService _readingTopicsService = readingTopicsService;
    private readonly IListeningTopicsService _listeningTopicsService = listeningTopicsService;
    private readonly IPromptService _promptService = promptService;

    public async Task<Result<string>> GenerateReadingFeedback(
        Guid topicId,
        ReadingTopicSubmitRequestDto submittedAnswer
    )
    {
        var topicSolutionQuery = await _readingTopicsService.GetTopicSolutionAsync(topicId);
        var topicSolution = topicSolutionQuery.ValueOrDefault;

        if (topicSolutionQuery.IsFailed || topicSolution is null)
        {
            _logger.LogWarning(
                CommonLoggingMessages.FailedToPerformActionWithId,
                "get reading topic solution",
                topicId
            );
            return new ProblemDetailsError(
                CommonErrorMessages.FailedTo($"get reading topic solution with ID: {topicId}")
            );
        }

        var prompt = GeneratePromptForReadingTopicFeedback(submittedAnswer, topicSolution);

        return await _promptService.GenerateContent(prompt);
    }

    private static string GeneratePromptForReadingTopicFeedback(
        ReadingTopicSubmitRequestDto submittedAnswer,
        ReadingTopicSolutionResponseDto topicSolution
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
                Based on this reading topic having:

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

    public async Task<Result<string>> GenerateListeningFeedback(
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

        return await _promptService.GenerateContent(prompt);
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
