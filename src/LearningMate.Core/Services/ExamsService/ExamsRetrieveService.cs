using FluentResults;
using LearningMate.Core.DTOs.ExamDTOs;
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

    public async Task<Result<ExamHasReadingTopicsGetRequestDto>> GetReadingTopicsOfExamIdAsync(
        Guid examId
    )
    {
        var getReadingTopicsResult = await _examsRepository.GetExamReadingTopicsAsync(examId);
        if (getReadingTopicsResult.IsFailed)
        {
            _logger.LogWarning(
                CommonLoggingMessages.FailedToPerformActionWithId,
                "get reading topics of ExamID",
                examId
            );
            return new ProblemDetailsError(
                CommonErrorMessages.FailedTo($"get reading topics of ExamID: {examId}")
            );
        }

        return _examMapper.MapExamToExamHasReadingTopicsGetRequestDto(getReadingTopicsResult.Value);
    }
}
