using FluentResults;
using LearningMate.Core.DTOs.ReadingTopicDTOs;
using LearningMate.Core.ErrorMessages;
using LearningMate.Core.Errors;
using LearningMate.Core.LoggingMessages;
using Microsoft.Extensions.Logging;

namespace LearningMate.Core.Services.ReadingTopicsService;

public partial class ReadingTopicsService
{
    public async Task<Result<ReadingTopicCreateResponseDto>> AddTopicAsync(
        ReadingTopicCreateRequestDto createRequestDto
    )
    {
        var topic = _readingTopicMapper.MapReadingTopicCreateRequestDtoToReadingTopic(createRequestDto);

        var addResult = await _readingTopicsRepository.AddTopicAsync(topic);

        if (addResult.IsFailed)
        {
            _logger.LogWarning(CommonLoggingMessages.FailedToCreate, "new reading topic");
            return new ProblemDetailsError(
                CommonErrorMessages.UnexpectedErrorHappenedDuringProcess("adding reading topic")
            );
        }

        var totalAffectedRows = addResult.ValueOrDefault;
        if (totalAffectedRows == 0)
        {
            _logger.LogWarning(CommonLoggingMessages.FailedToCreate, "new reading topic");
            return new ProblemDetailsError(CommonErrorMessages.FailedTo("adding reading topic"));
        }

        return _readingTopicMapper.MapReadingTopicToReadingTopicCreateResponseDto(topic);
    }
}
