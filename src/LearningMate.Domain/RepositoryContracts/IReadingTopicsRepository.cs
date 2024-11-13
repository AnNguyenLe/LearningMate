using FluentResults;
using LearningMate.Domain.Entities.Reading;

namespace LearningMate.Domain.RepositoryContracts;

public interface IReadingTopicsRepository
{
    Task<Result<int>> AddTopicAsync(ReadingTopic topic);
}
