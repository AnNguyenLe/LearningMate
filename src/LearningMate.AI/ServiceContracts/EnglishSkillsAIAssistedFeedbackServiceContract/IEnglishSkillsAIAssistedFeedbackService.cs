using FluentResults;
using LearningMate.Core.DTOs.ReadingTopicDTOs;

namespace LearningMate.AI.ServiceContracts.EnglishSkillsAIAssistedFeedbackServiceContract;

public interface IEnglishSkillsAIAssistedFeedbackService
{
    Task<Result<string>> GenerateReadingFeedback(
        Guid topicId,
        ReadingTopicSubmitRequestDto submittedAnswer
    );
}
