using FluentResults;
using LearningMate.Core.DTOs.SpeakingTopicAnswerDTOs;

namespace LearningMate.Core.ServiceContracts.SpeakingTopicAnswersServiceContract;

public interface ISpeakingTopicAnswersCreateService
{
    Task<Result<SpeakingTopicAnswerCreateResponseDto>> AddTopicAnswersAsync(
        SpeakingTopicAnswerCreateRequestDto createRequestDto
    );
}
