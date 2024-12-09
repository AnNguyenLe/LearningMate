using LearningMate.Core.Common.ExtensionMethods;
using LearningMate.Core.DTOs.SpeakingTopicDTOs;
using Microsoft.AspNetCore.Mvc;

namespace LearningMate.WebAPI.Controllers.v1.AIAssistedFeedbackController;

public partial class AIAssistedFeedbackController
{
    [HttpPost("speaking/{id}/feedback/generate", Name = nameof(GenerateSpeakingSubmissionFeedback))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<
        ActionResult<SpeakingTopicFeedbackResponseDto>
    > GenerateSpeakingSubmissionFeedback(
        [FromRoute] Guid id,
        [FromBody] SpeakingTopicSubmitRequestDto submitRequestDto
    )
    {
        var feedbackProcess = await _feedbackService.GenerateSpeakingFeedback(id, submitRequestDto);

        if (feedbackProcess.IsFailed)
        {
            _logger.LogWarning("Fail to generate feedback.");
            return feedbackProcess.Errors.ToDetailedBadRequest();
        }

        return feedbackProcess.Value!;
    }
}
