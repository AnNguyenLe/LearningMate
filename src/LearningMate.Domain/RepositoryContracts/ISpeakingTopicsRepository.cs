using FluentResults;
using LearningMate.Domain.Entities.Speaking;

namespace LearningMate.Domain.RepositoryContracts;

public interface ISpeakingTopicsRepository
{
    Task<Result<bool>> CheckSpeakingTopicExistsAsync(Guid topicId);
    Task<Result<int>> AddTopicAsync(SpeakingTopic topic);
}
