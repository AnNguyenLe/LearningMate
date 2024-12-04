using FluentResults;
using LearningMate.Core.DTOs.SpeakingTopicDTOs;

namespace LearningMate.Core.ServiceContracts.SpeakingTopicsServiceContract;

public interface ISpeakingTopicsCreateService
{
    Task<Result<SpeakingTopicCreateResponseDto>> AddTopicAsync(
        SpeakingTopicCreateRequestDto createRequestDto
    );
}
