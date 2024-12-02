using FluentResults;
using LearningMate.Domain.Entities.Writing;

namespace LearningMate.Domain.RepositoryContracts;

public interface IWritingTopicAnswersRepository
{
    Task<Result<int>> AddAnswerAsync(WritingTopicAnswer answer);
}
