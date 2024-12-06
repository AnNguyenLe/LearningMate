using FluentResults;
using LearningMate.Core.DTOs.ReadingTopicDTOs;
using LearningMate.Core.DTOs.ListeningTopicDTOs;

namespace LearningMate.AI.ServiceContracts.EnglishSkillsAIAssistedFeedbackServiceContract;

public interface IEnglishSkillsAIAssistedFeedbackService
{
    Task<Result<string>> GenerateReadingFeedback(
        Guid topicId,
        ReadingTopicSubmitRequestDto submittedAnswer
    );
    Task<Result<string>> GenerateListeningFeedback(
        Guid topicId,
        ListeningTopicSubmitRequestDto submittedAnswer
    );
}
