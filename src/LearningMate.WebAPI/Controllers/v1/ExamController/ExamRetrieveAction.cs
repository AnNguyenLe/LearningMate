using LearningMate.Core.Common.ExtensionMethods;
using LearningMate.Core.DTOs.Exam;
using LearningMate.Core.ErrorMessages;
using Microsoft.AspNetCore.Mvc;

namespace LearningMate.WebAPI.Controllers.v1.ExamController;

public partial class ExamController
{
    [HttpGet("exam/{id}", Name = nameof(ExamRetrieveAction))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ExamOverviewGetResponseDto>> ExamRetrieveAction([FromRoute] string id)
    {
        if (!Guid.TryParse(id, out var examId))
        {
            return BadRequestProblemDetails(CommonErrorMessages.InvalidIdFormat);
        }

        var getExamResult = await _examsService.GetExamOverviewByIdAsync(examId);

        if (getExamResult.IsFailed)
        {
            return getExamResult.Errors.ToDetailedBadRequest();
        }

        return getExamResult.Value;
    }

    // [HttpPost("exam/{examId}/reading/{id}", Name = nameof(ExamReadingSkillRetrieveAction))]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // public async Task<ActionResult<ExamGetResponseDto>> ExamReadingSkillRetrieveAction(
    //     [FromRoute] string examId,
    //     [FromRoute] string id
    // ) { }
}
