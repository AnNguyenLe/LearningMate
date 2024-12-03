using LearningMate.Core.Common.ExtensionMethods;
using LearningMate.Core.DTOs.SpeakingTopicDTOs;
using LearningMate.Core.ErrorMessages;
using LearningMate.Core.LoggingMessages;
using LearningMate.Domain.Entities.Speaking;
using Microsoft.AspNetCore.Mvc;
namespace LearningMate.WebAPI.Controllers.v1.SpeakingTopicController;
public partial class SpeakingTopicController
{
    [HttpPost("speaking/add", Name = nameof(CreateSpeakingTopic))]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SpeakingTopicCreateResponseDto>> CreateSpeakingTopic(
        [FromBody] SpeakingTopicCreateRequestDto createRequestDto
    )
    {
        var modelValidationResult = _speakingTopicCreateRequestValidator.Validate(createRequestDto);
        if (!modelValidationResult.IsValid)
        {
            return modelValidationResult.Errors.ToValidatingDetailedBadRequest(
                title: CommonErrorMessages.FailedTo("create speaking topic"),
                detail: CommonErrorMessages.MakeSureAllRequiredFieldsAreProperlyEnter
            );
        }
        var addingTopicResult = await _speakingTopicsService.AddTopicAsync(createRequestDto);
        if (addingTopicResult.IsFailed || addingTopicResult.ValueOrDefault is null)
        {
            _logger.LogWarning(CommonLoggingMessages.FailedToCreate, nameof(SpeakingTopic));
            return addingTopicResult.Errors.ToDetailedBadRequest();
        }
        var topic = addingTopicResult.Value;
        return Created();
    }
}