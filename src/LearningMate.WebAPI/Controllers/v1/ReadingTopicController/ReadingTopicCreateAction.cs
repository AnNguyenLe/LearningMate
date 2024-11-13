using LearningMate.Core.Common.ExtensionMethods;
using LearningMate.Core.DTOs.ReadingTopicDTOs;
using LearningMate.Core.ErrorMessages;
using LearningMate.Core.LoggingMessages;
using LearningMate.Domain.Entities.Reading;
using Microsoft.AspNetCore.Mvc;

namespace LearningMate.WebAPI.Controllers.v1.ReadingTopicController;

public partial class ReadingTopicController
{
    [HttpPost("reading/create", Name = nameof(CreateReadingTopic))]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ReadingTopicCreateResponseDto>> CreateReadingTopic(
        [FromBody] ReadingTopicCreateRequestDto createRequestDto
    )
    {
        var modelValidationResult = _readingTopicCreateRequestValidator.Validate(createRequestDto);

        if (!modelValidationResult.IsValid)
        {
            return modelValidationResult.Errors.ToValidatingDetailedBadRequest(
                title: CommonErrorMessages.FailedTo("create reading topic"),
                detail: CommonErrorMessages.MakeSureAllRequiredFieldsAreProperlyEnter
            );
        }

        var addingTopicResult = await _readingTopicsService.AddTopicAsync(createRequestDto);
        if (addingTopicResult.IsFailed || addingTopicResult.ValueOrDefault is null)
        {
            _logger.LogWarning(CommonLoggingMessages.FailedToCreate, nameof(ReadingTopic));
            return addingTopicResult.Errors.ToDetailedBadRequest();
        }

        var topic = addingTopicResult.Value;

        return Created();
    }

    // [HttpPost("reading/{id}/question/add", Name = nameof(CreateReadingTopicQuestions))]
    // [ProducesResponseType(StatusCodes.Status201Created)]
    // [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // public async Task<
    //     ActionResult<ReadingTopicQuestionCreateResponseDto>
    // > CreateReadingTopicQuestions(
    //     [FromRoute] string id,
    //     [FromBody] ReadingTopicQuestionCreateRequestDto createRequestDto
    // ) { }
}
