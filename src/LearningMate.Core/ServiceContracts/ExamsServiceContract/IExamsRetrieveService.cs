using FluentResults;
using LearningMate.Core.DTOs.Exam;

namespace LearningMate.Core.ServiceContracts.ExamsServiceContract;

public interface IExamsRetrieveService
{
    Task<Result<ExamOverviewGetResponseDto>> GetExamOverviewByIdAsync(Guid examId);
}
