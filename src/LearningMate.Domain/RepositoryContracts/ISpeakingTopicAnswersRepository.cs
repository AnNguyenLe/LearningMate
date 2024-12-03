using FluentResults;
using LearningMate.Domain.Entities.Speaking;

namespace LearningMate.Domain.RepositoryContracts;

public interface ISpeakingTopicAnswersRepository
{
    Task<Result<int>> AddAnswerAsync(SpeakingTopicAnswer answer);
}
