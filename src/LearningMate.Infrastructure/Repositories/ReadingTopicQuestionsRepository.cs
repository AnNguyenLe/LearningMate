using FluentResults;
using LearningMate.Domain.Entities.Reading;
using LearningMate.Domain.RepositoryContracts;

namespace LearningMate.Infrastructure.Repositories;

public class ReadingTopicQuestionsRepository : IReadingTopicQuestionsRepository
{
    public Task<Result<int>> AddQuestionAsync(ReadingTopicQuestion question)
    {
        throw new NotImplementedException();
    }
}
