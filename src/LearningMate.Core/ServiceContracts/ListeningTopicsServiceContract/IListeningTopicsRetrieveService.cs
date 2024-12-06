using FluentResults;
using LearningMate.Core.DTOs.ListeningTopicDTOs;
namespace LearningMate.Core.ServiceContracts.ListeningTopicsServiceContract;
public interface IListeningTopicsRetrieveService
{
    Task<Result<ListeningTopicSolutionResponseDto>> GetTopicSolutionAsync(
        Guid id
    );
}