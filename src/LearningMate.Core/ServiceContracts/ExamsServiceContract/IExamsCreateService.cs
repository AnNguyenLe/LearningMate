using FluentResults;
using LearningMate.Core.DTOs.ExamDTOs;

namespace LearningMate.Core.ServiceContracts.ExamsServiceContract;

public interface IExamsCreateService
{
    Task<Result<ExamCreateResponseDto>> CreateExamAsync(ExamCreateRequestDto createRequestDto);
}
