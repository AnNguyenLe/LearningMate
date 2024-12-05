using System.Text.Json;
using LearningMate.Core.Common.ExtensionMethods;
using LearningMate.Core.DTOs.ReadingTopicDTOs;
using Microsoft.AspNetCore.Mvc;

namespace LearningMate.WebAPI.Controllers.v1.AIAssistedFeedbackController;

public class ReadingTopicFeedback
{
    public string? Question { get; set; }
    public string? SelectedAnswer { get; set; }
    public string? CorrectAnswer { get; set; }
    public string? Explanation { get; set; }
}

public partial class AIAssistedFeedbackController
{
    [HttpPost("reading/{id}/feedback/generate", Name = nameof(GenerateReadingSubmissionFeedback))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<ReadingTopicFeedback>>> GenerateReadingSubmissionFeedback(
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

        var mainInfo = RemoveJsonDelimiters(feedbackProcess.Value);

        var feedback = JsonSerializer.Deserialize<List<ReadingTopicFeedback>>(mainInfo);

        return feedback!;
    }

    public static string RemoveJsonDelimiters(string input)
    {
        var OPEN_SQUARE_BRACKET = "[";
        var CLOSE_SQUARE_BRACKET = "]";
        var totalCharacterAtTheBeginning = 8; // ```json\n
        var totalCharacterAtTheEnd = 4; // \n```
        if (!input.StartsWith(OPEN_SQUARE_BRACKET) && !input.EndsWith(CLOSE_SQUARE_BRACKET))
        {
            // Remove the "```json" at the beginning and "```" at the end
            input = input.Substring(
                totalCharacterAtTheBeginning,
                input.Length - (totalCharacterAtTheBeginning + totalCharacterAtTheEnd)
            );
        }

        return input;
    }
}
