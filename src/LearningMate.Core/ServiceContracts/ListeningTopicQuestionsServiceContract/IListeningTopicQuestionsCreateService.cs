using FluentResults;
using LearningMate.Core.DTOs.ListeningTopicQuestionDTOs;

namespace LearningMate.Core.ServiceContracts.ListeningTopicQuestionsServiceContract;

public interface IListeningTopicQuestionsCreateService
{
    Task<Result<ListeningTopicQuestionCreateResponseDto>> AddTopicQuestionsAsync(
        ListeningTopicQuestionCreateRequestDto createRequestDto
    );
}
