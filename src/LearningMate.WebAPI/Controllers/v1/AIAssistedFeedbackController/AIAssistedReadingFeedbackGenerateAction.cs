using LearningMate.Core.Common.ExtensionMethods;
using LearningMate.Core.DTOs.ReadingTopicDTOs;
using Microsoft.AspNetCore.Mvc;

namespace LearningMate.WebAPI.Controllers.v1.AIAssistedFeedbackController;

public partial class AIAssistedFeedbackController
{
    [HttpPost("reading/{id}/feedback/generate", Name = nameof(GenerateReadingSubmissionFeedback))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<
        ActionResult<List<ReadingTopicQuestionFeedback>>
    > GenerateReadingSubmissionFeedback(
        [FromRoute] Guid id,
        [FromBody] ReadingTopicSubmitRequestDto submitRequestDto
    )
    {
        var feedbackProcess = await _feedbackService.GenerateReadingFeedback(id, submitRequestDto);

        if (feedbackProcess.IsFailed)
        {
            _logger.LogWarning("Fail to generate feedback.");
            return feedbackProcess.Errors.ToDetailedBadRequest();
        }

        return feedbackProcess.ValueOrDefault;
    }
}
