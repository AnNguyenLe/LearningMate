using LearningMate.Core.Common.ExtensionMethods;
using LearningMate.Core.DTOs.WritingTopicDTOs;
using Microsoft.AspNetCore.Mvc;

namespace LearningMate.WebAPI.Controllers.v1.AIAssistedFeedbackController;

public partial class AIAssistedFeedbackController
{
    [HttpPost("writing/{id}/feedback/generate", Name = nameof(GenerateWritingSubmissionFeedback))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<
        ActionResult<WritingTopicFeedbackResponseDto>
    > GenerateWritingSubmissionFeedback(
        [FromRoute] Guid id,
        [FromBody] WritingTopicSubmitRequestDto submitRequestDto
    )
    {
        var feedbackProcess = await _feedbackService.GenerateWritingFeedback(id, submitRequestDto);

        if (feedbackProcess.IsFailed)
        {
            _logger.LogWarning("Fail to generate feedback.");
            return feedbackProcess.Errors.ToDetailedBadRequest();
        }

        return feedbackProcess.Value!;
    }
}
