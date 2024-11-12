using FluentResults;
using LearningMate.Core.DTOs.Exam;
using LearningMate.Core.ErrorMessages;
using LearningMate.Core.Errors;
using LearningMate.Core.LoggingMessages;
using Microsoft.Extensions.Logging;

namespace LearningMate.Core.Services.ExamsService;

public partial class ExamsService
{
    public async Task<Result<ExamOverviewGetResponseDto>> GetExamOverviewByIdAsync(Guid examId)
    {
        var getExamResult = await _examsRepository.GetExamOverviewAsync(examId);
        if (getExamResult.IsFailed)
        {
            _logger.LogWarning(
                CommonLoggingMessages.FailedToPerformActionWithId,
                "get exam",
                examId
            );
            return new ProblemDetailsError(
                CommonErrorMessages.FailedTo($"get exam with Exam ID: {examId}")
            );
        }

        return _examMapper.MapExamToExamOverviewGetResponseDto(getExamResult.Value);
    }
}
