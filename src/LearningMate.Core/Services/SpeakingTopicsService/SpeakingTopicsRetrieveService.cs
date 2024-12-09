using FluentResults;
using LearningMate.Core.ErrorMessages;
using LearningMate.Core.Errors;
using LearningMate.Core.LoggingMessages;
using LearningMate.Domain.Entities.Speaking;
using Microsoft.Extensions.Logging;
namespace LearningMate.Core.Services.SpeakingTopicsService;
public partial class SpeakingTopicsService
{
    public async Task<Result<SpeakingTopic>> GetTopicAsync(Guid id)
    {
        var queryResult = await _speakingTopicsRepository.GetTopicByIdAsync(id);
        if (queryResult.IsFailed || queryResult.ValueOrDefault is null)
        {
            _logger.LogWarning(
                CommonLoggingMessages.FailedToPerformActionWithId,
                "get speaking topic with ID",
                id
            );
            return new ProblemDetailsError(
                CommonErrorMessages.FailedTo($"get speaking topic with ID: {id}")
            );
        }
        return queryResult.ValueOrDefault;
    }
}