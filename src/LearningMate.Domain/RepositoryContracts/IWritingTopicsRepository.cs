using FluentResults;
using LearningMate.Domain.Entities.Writing;

namespace LearningMate.Domain.RepositoryContracts;

public interface IWritingTopicsRepository
{
    Task<Result<bool>> CheckReadingTopicExistsAsync(Guid topicId);
    Task<Result<int>> AddTopicAsync(WritingTopic topic);
    Task<Result<WritingTopic>> GetTopicByIdAsync(Guid topicId);
}
