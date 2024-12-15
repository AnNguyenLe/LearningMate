using FluentResults;
using LearningMate.Core.DTOs.ListeningTopicDTOs;
using LearningMate.Domain.Entities.Listening;

namespace LearningMate.Core.ServiceContracts.ListeningTopicsServiceContract;

public interface IListeningTopicsRetrieveService
{
    Task<Result<ListeningTopicSolutionResponseDto>> GetTopicSolutionAsync(Guid id);
    Task<Result<ListeningTopic>> GetTopicContent(Guid id);
}
