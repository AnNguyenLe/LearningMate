using FluentResults;
using LearningMate.Domain.Entities.Listening;

namespace LearningMate.Domain.RepositoryContracts;

public interface IListeningTopicQuestionsRepository
{
    Task<Result<int>> AddQuestionAsync(ListeningTopicQuestion question);
}
