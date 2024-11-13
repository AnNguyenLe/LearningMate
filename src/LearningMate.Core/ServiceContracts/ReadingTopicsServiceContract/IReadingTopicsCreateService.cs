using FluentResults;
using LearningMate.Core.DTOs.ReadingTopicDTOs;

namespace LearningMate.Core.ServiceContracts.ReadingTopicsServiceContract;

public interface IReadingTopicsCreateService
{
    Task<Result<ReadingTopicCreateResponseDto>> AddTopicAsync(
        ReadingTopicCreateRequestDto createRequestDto
    );
}
