using FluentResults;
using LearningMate.Domain.Entities.Writing;

namespace LearningMate.Core.ServiceContracts.WritingTopicsServiceContract;

public interface IWritingTopicsRetrieveService
{
    Task<Result<WritingTopic>> GetTopicAsync(Guid id);
}
