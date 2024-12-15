using FluentResults;
using LearningMate.Domain.Entities.Reading;

namespace LearningMate.Domain.RepositoryContracts;

public interface IReadingTopicsRepository
{
    Task<Result<bool>> CheckReadingTopicExistsAsync(Guid topicId);
    Task<Result<int>> AddTopicAsync(ReadingTopic topic);
    Task<Result<ReadingTopic>> GetReadingTopicWithSolutionById(Guid id);
}
