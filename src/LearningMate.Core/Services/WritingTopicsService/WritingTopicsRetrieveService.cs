using FluentResults;
using LearningMate.Core.ErrorMessages;
using LearningMate.Core.Errors;
using LearningMate.Core.LoggingMessages;
using LearningMate.Domain.Entities.Writing;
using Microsoft.Extensions.Logging;

namespace LearningMate.Core.Services.WritingTopicsService;

public partial class WritingTopicsService
{
    public async Task<Result<WritingTopic>> GetTopicAsync(Guid id)
    {
        var queryResult = await _writingTopicsRepository.GetTopicByIdAsync(id);
        if (queryResult.IsFailed || queryResult.ValueOrDefault is null)
        {
            _logger.LogWarning(
                CommonLoggingMessages.FailedToPerformActionWithId,
                "get writing topic with ID",
                id
            );
            return new ProblemDetailsError(
                CommonErrorMessages.FailedTo($"get writing topic with ID: {id}")
            );
        }

        return queryResult.ValueOrDefault;
    }
}
