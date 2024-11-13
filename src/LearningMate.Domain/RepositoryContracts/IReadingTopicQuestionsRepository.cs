using FluentResults;
using LearningMate.Domain.Entities.Reading;

namespace LearningMate.Domain.RepositoryContracts;

public interface IReadingTopicQuestionsRepository
{
    Task<Result<int>> AddQuestionAsync(ReadingTopicQuestion question);
}
