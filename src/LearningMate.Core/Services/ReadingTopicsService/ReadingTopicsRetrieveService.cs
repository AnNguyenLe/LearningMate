using FluentResults;
using LearningMate.Core.DTOs.ReadingTopicDTOs;
using LearningMate.Core.ErrorMessages;
using LearningMate.Core.Errors;
using LearningMate.Core.LoggingMessages;
using LearningMate.Domain.Entities.Reading;
using Microsoft.Extensions.Logging;

namespace LearningMate.Core.Services.ReadingTopicsService;

public partial class ReadingTopicsService
{
    public async Task<Result<ReadingTopicSolutionResponseDto>> GetTopicSolutionAsync(Guid id)
    {
        var checkExistResult = await _readingTopicsRepository.CheckReadingTopicExistsAsync(id);

        if (checkExistResult.IsFailed)
        {
            return new ProblemDetailsError(
                CommonErrorMessages.RecordNotFoundWithId(nameof(ReadingTopic), id)
            );
        }
        var topicRetrieveResult = await _readingTopicsRepository.GetReadingTopicWithSolutionById(
            id
        );

        if (topicRetrieveResult.IsFailed)
        {
            _logger.LogWarning(
                CommonLoggingMessages.FailedToPerformActionWithId,
                "retrieve reading topic",
                id
            );
            return new ProblemDetailsError(CommonErrorMessages.FailedTo("retrieve reading topic"));
        }

        return _readingTopicMapper.MapReadingTopicToReadingTopicSolutionResponseDto(
            topicRetrieveResult.Value
        );
    }
}
