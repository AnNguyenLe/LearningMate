using FluentResults;
using LearningMate.Core.DTOs.WritingTopicAnswerDTOs;

namespace LearningMate.Core.ServiceContracts.WritingTopicAnswersServiceContract;

public interface IWritingTopicAnswersCreateService
{
    Task<Result<WritingTopicAnswerCreateResponseDto>> AddTopicAnswersAsync(
        WritingTopicAnswerCreateRequestDto createRequestDto
    );
}
