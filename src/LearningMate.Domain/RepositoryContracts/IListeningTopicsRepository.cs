using FluentResults;
using LearningMate.Domain.Entities.Listening;

namespace LearningMate.Domain.RepositoryContracts;

public interface IListeningTopicsRepository
{
    Task<Result<bool>> CheckListeningTopicExistsAsync(Guid topicId);
    Task<Result<int>> AddTopicAsync(ListeningTopic topic);
    Task<Result<ListeningTopic>> GetListeningTopicById(Guid id);
}
