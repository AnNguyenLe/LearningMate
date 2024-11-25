using FluentResults;
using LearningMate.Core.DTOs.ExamDTOs;

namespace LearningMate.Core.ServiceContracts.ExamsServiceContract;

public interface IExamsRetrieveService
{
    Task<Result<ExamOverviewGetResponseDto>> GetExamOverviewByIdAsync(Guid examId);
    Task<Result<ExamHasListeningTopicsGetRequestDto>> GetListeningTopicsOfExamIdAsync(Guid examId);
    Task<Result<ExamHasReadingTopicsGetRequestDto>> GetReadingTopicsOfExamIdAsync(Guid examId);
}
