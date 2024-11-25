using FluentResults;
using LearningMate.Core.DTOs.ListeningTopicDTOs;

namespace LearningMate.Core.ServiceContracts.ListeningTopicsServiceContract;

public interface IListeningTopicsCreateService
{
    Task<Result<ListeningTopicCreateResponseDto>> AddTopicAsync(
        ListeningTopicCreateRequestDto createRequestDto
    );
}
