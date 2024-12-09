using System.Text.Json;
using FluentResults;
using LearningMate.AI.Extensions;
using LearningMate.Core.DTOs.WritingTopicDTOs;
using LearningMate.Core.ErrorMessages;
using LearningMate.Core.Errors;
using LearningMate.Core.LoggingMessages;
using LearningMate.Domain.Entities.Writing;
using Microsoft.Extensions.Logging;

namespace LearningMate.AI.Services.EnglishSkillsAIAssistedFeedbackService;

public partial class EnglishSkillsAIAssistedFeedbackService
{
    public async Task<Result<WritingTopicFeedbackResponseDto>> GenerateWritingFeedback(
        Guid topicId,
        WritingTopicSubmitRequestDto submittedAnswer
    )
    {
        var topicQueryResult = await _writingTopicsService.GetTopicAsync(topicId);

        if (topicQueryResult.IsFailed)
        {
            _logger.LogWarning(
                CommonLoggingMessages.FailedToPerformActionWithId,
                "get writing topic with ID",
                topicId
            );
            return new ProblemDetailsError(
                CommonErrorMessages.FailedTo($"get writing topic with ID: {topicId}")
            );
        }

        var prompt = GeneratePromptForWritingingTopicFeedback(
            submittedAnswer,
            topicQueryResult.Value
        );

        var contentGeneratingProcess = await _promptService.GenerateContent(prompt);

        if (contentGeneratingProcess.IsFailed)
        {
            _logger.LogWarning(
                CommonLoggingMessages.FailedToPerformActionWithId,
                "generating feedback for writing topic",
                topicId
            );
            return new ProblemDetailsError(
                CommonErrorMessages.FailedTo($"generating feedback for writing topic: {topicId}")
            );
        }

        return JsonSerializer.Deserialize<WritingTopicFeedbackResponseDto>(
            contentGeneratingProcess.Value.RemoveJsonDelimiters()
        )!;
    }

    private static string GeneratePromptForWritingingTopicFeedback(
        WritingTopicSubmitRequestDto submittedAnswer,
        WritingTopic topic
    )
    {
        return $"""
                Based on this writing topic having:

                Requirement: {topic.Content}
                Category: {topic.Category}
                Resources in the form of json-stringified if any: {topic.SerializedResourcesUrl}

                and the examinee writing submission: 
                {submittedAnswer.Essay}

                Write me back in JSON format and each item of the list using the following pattern so that I can easily destringify.
                Apply for all questions. And the final result should be in clean JSON format:

                Feedback: <your-feedback-on-examinee-writing-submission-and-how-can-examinee-do-better-next-time>,
                SampleEssay: <your-sample-essay-based-on-requirement-and-resources-attached>,
            """;
    }
}
