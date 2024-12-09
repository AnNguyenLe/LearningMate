using FluentResults;
using LearningMate.Core.DTOs.ListeningTopicDTOs;
using LearningMate.Core.DTOs.ReadingTopicDTOs;
using LearningMate.Core.DTOs.WritingTopicDTOs;

namespace LearningMate.AI.ServiceContracts.EnglishSkillsAIAssistedFeedbackServiceContract;

public interface IEnglishSkillsAIAssistedFeedbackService
{
    Task<Result<List<ReadingTopicQuestionFeedback>>> GenerateReadingFeedback(
        Guid topicId,
        ReadingTopicSubmitRequestDto submittedAnswer
    );
    Task<Result<List<ListeningTopicQuestionFeedback>>> GenerateListeningFeedback(
        Guid topicId,
        ListeningTopicSubmitRequestDto submittedAnswer
    );
    Task<Result<WritingTopicFeedbackResponseDto>> GenerateWritingFeedback(
        Guid topicId,
        WritingTopicSubmitRequestDto submittedAnswer
    );
}
