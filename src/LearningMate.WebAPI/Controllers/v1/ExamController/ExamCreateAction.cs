using LearningMate.Core.Common.ExtensionMethods;
using LearningMate.Core.DTOs.ExamDTOs;
using LearningMate.Core.ErrorMessages;
using LearningMate.Core.LoggingMessages;
using LearningMate.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LearningMate.WebAPI.Controllers.v1.ExamController;

public partial class ExamController
{
    [HttpPost("exam/create", Name = nameof(CreateExam))]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ExamCreateResponseDto>> CreateExam(
        [FromBody] ExamCreateRequestDto createRequestDto
    )
    {
        var modelValidationResult = _examCreateRequestValidator.Validate(createRequestDto);

        if (!modelValidationResult.IsValid)
        {
            return modelValidationResult.Errors.ToValidatingDetailedBadRequest(
                title: CommonErrorMessages.FailedTo("create exam"),
                detail: CommonErrorMessages.MakeSureAllRequiredFieldsAreProperlyEnter
            );
        }

        var getUserIdResult = TryGetUserId();
        if (getUserIdResult.IsFailed)
        {
            _logger.LogWarning(CommonErrorMessages.UnableToIdentifyUser);
            return getUserIdResult.Errors.ToDetailedBadRequest();
        }

        var createExamResult = await _examsService.CreateExamAsync(createRequestDto);

        if (createExamResult.IsFailed || createExamResult.ValueOrDefault is null)
        {
            _logger.LogWarning(CommonLoggingMessages.FailedToCreate, nameof(Exam));
            return createExamResult.Errors.ToDetailedBadRequest();
        }

        var exam = createExamResult.Value;

        return CreatedAtRoute(
            nameof(RetriveExamOverview),
            new { id = exam.Id },
            createExamResult.Value
        );
    }
}
