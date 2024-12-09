using FluentResults;
using LearningMate.Domain.Entities.Speaking;
namespace LearningMate.Core.ServiceContracts.SpeakingTopicsServiceContract;
public interface ISpeakingTopicsRetrieveService
{
    Task<Result<SpeakingTopic>> GetTopicAsync(Guid id);
}