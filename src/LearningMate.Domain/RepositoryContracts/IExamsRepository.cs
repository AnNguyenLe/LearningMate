using FluentResults;
using LearningMate.Domain.Entities;

namespace LearningMate.Domain.RepositoryContracts;

public interface IExamsRepository
{
    Task<Result<int>> AddExamAsync(Exam exam);
    Task<Result<Exam>> GetExamOverviewAsync(Guid examId);
    Task<Result<Exam>> GetExamReadingTopicsAsync(Guid examId);
}
