using FluentResults;
using LearningMate.Core.DTOs.WritingTopicDTOs;

namespace LearningMate.Core.ServiceContracts.WritingTopicsServiceContract;

public interface IWritingTopicsCreateService
{
    Task<Result<WritingTopicCreateResponseDto>> AddTopicAsync(
        WritingTopicCreateRequestDto createRequestDto
    );
}
