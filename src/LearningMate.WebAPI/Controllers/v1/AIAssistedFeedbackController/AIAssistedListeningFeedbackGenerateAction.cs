using System.Text.Json;
using LearningMate.Core.Common.ExtensionMethods;
using LearningMate.Core.DTOs.ListeningTopicDTOs;
using Microsoft.AspNetCore.Mvc;
namespace LearningMate.WebAPI.Controllers.v1.AIAssistedFeedbackController;
public class ListeningTopicFeedback
{
    public string? Question { get; set; }
    public string? SelectedAnswer { get; set; }
    public string? CorrectAnswer { get; set; }
    public string? Explanation { get; set; }
}
public partial class AIAssistedFeedbackController
{
    [HttpPost("listening/{id}/feedback/generate", Name = nameof(GenerateListeningSubmissionFeedback))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<ListeningTopicFeedback>>> GenerateListeningSubmissionFeedback(
        [FromRoute] Guid id,
        [FromBody] ListeningTopicSubmitRequestDto submitRequestDto
    )
    {
        var feedbackProcess = await _feedbackService.GenerateListeningFeedback(id, submitRequestDto);
        if (feedbackProcess.IsFailed)
        {
            _logger.LogWarning("Fail to generate feedback.");
            return feedbackProcess.Errors.ToDetailedBadRequest();
        }
        var mainInfo = RemoveJsonDelimiters(feedbackProcess.Value);
        var feedback = JsonSerializer.Deserialize<List<ListeningTopicFeedback>>(mainInfo);
        return feedback!;
    }
}