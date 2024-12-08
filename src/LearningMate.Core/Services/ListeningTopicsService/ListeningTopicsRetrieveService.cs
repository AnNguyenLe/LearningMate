using FluentResults;
using LearningMate.Core.DTOs.ListeningTopicDTOs;
using LearningMate.Core.ErrorMessages;
using LearningMate.Core.Errors;
using LearningMate.Core.LoggingMessages;
using LearningMate.Domain.Entities.Listening;
using Microsoft.Extensions.Logging;
namespace LearningMate.Core.Services.ListeningTopicsService;
public partial class ListeningTopicsService
{
    public async Task<Result<ListeningTopicSolutionResponseDto>> GetTopicSolutionAsync(Guid id)
    {
        var checkExistResult = await _listeningTopicsRepository.CheckListeningTopicExistsAsync(id);
        if (checkExistResult.IsFailed)
        {
            return new ProblemDetailsError(
                CommonErrorMessages.RecordNotFoundWithId(nameof(ListeningTopic), id)
            );
        }
        var topicRetrieveResult = await _listeningTopicsRepository.GetListeningTopicById(id);
        if (topicRetrieveResult.IsFailed)
        {
            _logger.LogWarning(
                CommonLoggingMessages.FailedToPerformActionWithId,
                "retrieve listening topic",
                id
            );
            return new ProblemDetailsError(CommonErrorMessages.FailedTo("retrieve listening topic"));
        }
        return _listeningTopicMapper.MapListeningTopicToListeningTopicSolutionResponseDto(
            topicRetrieveResult.Value
        );
    }
}