using LearningMate.Core.Common.ExtensionMethods;
using LearningMate.Core.DTOs.ExamDTOs;
using LearningMate.Core.ErrorMessages;
using Microsoft.AspNetCore.Mvc;

namespace LearningMate.WebAPI.Controllers.v1.ExamController;

public partial class ExamController
{
    [HttpGet("exam/{id}", Name = nameof(RetriveExamOverview))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ExamOverviewGetResponseDto>> RetriveExamOverview(
        [FromRoute] string id
    )
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

    [HttpGet("exam/{id}/writing", Name = nameof(RetrieveExamWritingTopics))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ExamHasWritingTopicsGetRequestDto>> RetrieveExamWritingTopics(
        [FromRoute] string id
    )
    {
        if (!Guid.TryParse(id, out var examId))
        {
            return BadRequestProblemDetails(CommonErrorMessages.InvalidIdFormat);
        }

        var getTopicsResult = await _examsService.GetWritingTopicsOfExamIdAsync(examId);

        if (getTopicsResult.IsFailed)
        {
            return getTopicsResult.Errors.ToDetailedBadRequest();
        }

        return getTopicsResult.Value;
    }

    [HttpGet("exam/{id}/listening", Name = nameof(RetrieveExamListeningTopics))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<
        ActionResult<ExamHasListeningTopicsGetRequestDto>
    > RetrieveExamListeningTopics([FromRoute] string id)
    {
        if (!Guid.TryParse(id, out var examId))
        {
            return BadRequestProblemDetails(CommonErrorMessages.InvalidIdFormat);
        }

        var getListeningTopicsResult = await _examsService.GetListeningTopicsOfExamIdAsync(examId);

        if (getListeningTopicsResult.IsFailed)
        {
            return getListeningTopicsResult.Errors.ToDetailedBadRequest();
        }

        return getListeningTopicsResult.Value;
    }

    [HttpGet("exam/{id}/reading", Name = nameof(RetrieveExamReadingTopics))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ExamHasReadingTopicsGetRequestDto>> RetrieveExamReadingTopics(
        [FromRoute] string id
    )
    {
        if (!Guid.TryParse(id, out var examId))
        {
            return BadRequestProblemDetails(CommonErrorMessages.InvalidIdFormat);
        }

        var getReadingTopicsResult = await _examsService.GetReadingTopicsOfExamIdAsync(examId);

        if (getReadingTopicsResult.IsFailed)
        {
            return getReadingTopicsResult.Errors.ToDetailedBadRequest();
        }

        return getReadingTopicsResult.Value;
    }
}
