using FluentResults;
using LearningMate.Core.DTOs.ReadingTopicQuestionDTOs;

namespace LearningMate.Core.ServiceContracts.ReadingTopicQuestionsServiceContract;

public interface IReadingTopicQuestionsCreateService
{
    Task<Result<ReadingTopicQuestionCreateResponseDto>> AddTopicQuestionsAsync(
        ReadingTopicQuestionCreateRequestDto createRequestDto
    );
}
