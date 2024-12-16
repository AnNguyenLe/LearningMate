using LearningMate.Core.Common.ExtensionMethods;
using LearningMate.Core.Errors;
using Microsoft.AspNetCore.Mvc;

namespace LearningMate.WebAPI.Controllers.v1.ListeningTopicController;

public partial class ListeningTopicController
{
    [HttpGet("listening/{id}", Name = nameof(RetrieveListeningTopic))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RetrieveListeningTopic([FromRoute] Guid id)
    {
        var getTopicContent = await _listeningTopicsService.GetTopicContent(id);

        if (getTopicContent.IsFailed)
        {
            return getTopicContent.Errors.ToDetailedBadRequest();
        }

        var topic = getTopicContent.Value;

        var topicContent = topic.Content;

        if (string.IsNullOrWhiteSpace(topicContent))
        {
            return Problem(detail: "Topic content is empty");
        }

        var audioStream = await _textToSpeechService.SynthesizeAsync(topicContent);

        var fileDownloadName = $"{topic.Title}.mp3";

        return File(audioStream.Value, "audio/mpeg", fileDownloadName);
    }
}
