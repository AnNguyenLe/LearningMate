using FluentResults;
using LearningMate.Core.DTOs.ExamDTOs;
using LearningMate.Core.ErrorMessages;
using LearningMate.Core.Errors;
using LearningMate.Core.LoggingMessages;
using LearningMate.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace LearningMate.Core.Services.ExamsService;

public partial class ExamsService
{
    public async Task<Result<ExamCreateResponseDto>> CreateExamAsync(
        ExamCreateRequestDto createRequestDto
    )
    {
        var createExamText = "create exam";

        var exam = _examMapper.MapExamCreateRequestDtoToExam(createRequestDto);

        var createResult = await _examsRepository.AddExamAsync(exam);

        if (createResult.IsFailed)
        {
            _logger.LogWarning(CommonLoggingMessages.FailedToCreate, nameof(Exam));
            return new ProblemDetailsError(
                CommonErrorMessages.UnexpectedErrorHappenedDuringProcess(createExamText)
            );
        }

        var totalAffectedRows = createResult.ValueOrDefault;
        if (totalAffectedRows == 0)
        {
            _logger.LogWarning(CommonLoggingMessages.FailedToCreate, nameof(Exam));
            return new ProblemDetailsError(CommonErrorMessages.FailedTo(createExamText));
        }

        return _examMapper.MapExamToExamCreateResponseDto(exam);
    }
}
