using System.Text.Json;
using FluentResults;
using LearningMate.AI.Extensions;
using LearningMate.Core.DTOs.SpeakingTopicDTOs;
using LearningMate.Core.ErrorMessages;
using LearningMate.Core.Errors;
using LearningMate.Core.LoggingMessages;
using LearningMate.Domain.Entities.Speaking;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace LearningMate.AI.Services.EnglishSkillsAIAssistedFeedbackService;

public partial class EnglishSkillsAIAssistedFeedbackService
{
    public async Task<Result<SpeakingTopicFeedbackResponseDto>> GenerateSpeakingFeedback(
        Guid topicId,
        IFormFile audioFile
    )
    {
        var topicQueryResult = await _speakingTopicsService.GetTopicAsync(topicId);

        if (topicQueryResult.IsFailed)
        {
            _logger.LogWarning(
                CommonLoggingMessages.FailedToPerformActionWithId,
                "get speaking topic with ID",
                topicId
            );
            return new ProblemDetailsError(
                CommonErrorMessages.FailedTo($"get speaking topic with ID: {topicId}")
            );
        }

        var transcribeProcess = await _speechService.TranscribeAudioAsync(audioFile);

        if (transcribeProcess.IsFailed || transcribeProcess.ValueOrDefault is null)
        {
            _logger.LogWarning("Error processing audio");
            return new ProblemDetailsError(CommonErrorMessages.FailedTo("processing audio"));
        }

        if (transcribeProcess.ValueOrDefault.Trim() == string.Empty)
        {
            _logger.LogWarning("Transcription is empty.");
            return new ProblemDetailsError(
                title: "Cannot giving feedback as the transcription is empty",
                detail: "Make sure you mic is working properly or enable audio recording"
            );
        }

        var prompt = GeneratePromptForSpeakingTopicFeedback(
            transcribeProcess.Value,
            topicQueryResult.Value
        );

        var contentGeneratingProcess = await _promptService.GenerateContent(prompt);

        if (contentGeneratingProcess.IsFailed)
        {
            _logger.LogWarning(
                CommonLoggingMessages.FailedToPerformActionWithId,
                "generating feedback for speaking topic",
                topicId
            );
            return new ProblemDetailsError(
                CommonErrorMessages.FailedTo($"generating feedback for speaking topic: {topicId}")
            );
        }

        return JsonSerializer.Deserialize<SpeakingTopicFeedbackResponseDto>(
            contentGeneratingProcess.Value.RemoveJsonDelimiters()
        )!;
    }

    private static string GeneratePromptForSpeakingTopicFeedback(
        string transcription,
        SpeakingTopic topic
    )
    {
        return $"""
                Based on this speaking topic having:

                Requirement: {topic.Content}
                Category: {topic.Category}
                Resources in the form of json-stringified if any: {topic.SerializedResourcesUrl}

                and the examinee speaking submission:
                {transcription}

                Write me back in JSON format and each item of the list using the following pattern so that I can easily destringify.
                Apply for all questions. And the final result should be in clean JSON format:

                Feedback: <your-feedback-on-examinee-speaking-submission-and-how-can-examinee-do-better-next-time>,
                SampleEssay: <your-sample-essay-based-on-requirement-and-resources-attached>,
            """;
    }
}
