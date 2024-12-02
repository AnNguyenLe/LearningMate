using LearningMate.Core.Common.ExtensionMethods;
using LearningMate.Core.DTOs.WritingTopicDTOs;
using LearningMate.Core.ErrorMessages;
using LearningMate.Core.LoggingMessages;
using LearningMate.Domain.Entities.Writing;
using Microsoft.AspNetCore.Mvc;

namespace LearningMate.WebAPI.Controllers.v1.WritingTopicController;

public partial class WritingTopicController
{
    [HttpPost("writing/add", Name = nameof(CreateWritingTopic))]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<WritingTopicCreateResponseDto>> CreateWritingTopic(
        [FromBody] WritingTopicCreateRequestDto createRequestDto
    )
    {
        var modelValidationResult = _writingTopicCreateRequestValidator.Validate(createRequestDto);

        if (!modelValidationResult.IsValid)
        {
            return modelValidationResult.Errors.ToValidatingDetailedBadRequest(
                title: CommonErrorMessages.FailedTo("create writing topic"),
                detail: CommonErrorMessages.MakeSureAllRequiredFieldsAreProperlyEnter
            );
        }

        var addingTopicResult = await _writingTopicsService.AddTopicAsync(createRequestDto);
        if (addingTopicResult.IsFailed || addingTopicResult.ValueOrDefault is null)
        {
            _logger.LogWarning(CommonLoggingMessages.FailedToCreate, nameof(WritingTopic));
            return addingTopicResult.Errors.ToDetailedBadRequest();
        }

        var topic = addingTopicResult.Value;

        return Created();
    }
}
