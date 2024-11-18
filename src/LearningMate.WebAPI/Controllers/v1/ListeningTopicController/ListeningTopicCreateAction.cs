using LearningMate.Core.Common.ExtensionMethods;
using LearningMate.Core.DTOs.ListeningTopicDTOs;
using LearningMate.Core.DTOs.ListeningTopicQuestionDTOs;
using LearningMate.Core.ErrorMessages;
using LearningMate.Core.LoggingMessages;
using LearningMate.Domain.Entities.Listening;
using Microsoft.AspNetCore.Mvc;

namespace LearningMate.WebAPI.Controllers.v1.ListeningTopicController;

public partial class ListeningTopicController
{
    [HttpPost("listening/add", Name = nameof(CreateListeningTopic))]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ListeningTopicCreateResponseDto>> CreateListeningTopic(
        [FromBody] ListeningTopicCreateRequestDto createRequestDto
    )
    {
        var modelValidationResult = _listeningTopicCreateRequestValidator.Validate(createRequestDto);

        if (!modelValidationResult.IsValid)
        {
            return modelValidationResult.Errors.ToValidatingDetailedBadRequest(
                title: CommonErrorMessages.FailedTo("create listening topic"),
                detail: CommonErrorMessages.MakeSureAllRequiredFieldsAreProperlyEnter
            );
        }

        var addingTopicResult = await _listeningTopicsService.AddTopicAsync(createRequestDto);
        if (addingTopicResult.IsFailed || addingTopicResult.ValueOrDefault is null)
        {
            _logger.LogWarning(CommonLoggingMessages.FailedToCreate, nameof(ListeningTopic));
            return addingTopicResult.Errors.ToDetailedBadRequest();
        }

        var topic = addingTopicResult.Value;

        return Created();
    }
    
    [HttpPost("listening/question/add", Name = nameof(CreateListeningTopicQuestion))]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<
        ActionResult<ListeningTopicQuestionCreateResponseDto>
    > CreateListeningTopicQuestion([FromBody] ListeningTopicQuestionCreateRequestDto createRequestDto)
    {
        var modelValidationResult = _listeningTopicQuestionCreateRequestValidator.Validate(
            createRequestDto
        );

        if (!modelValidationResult.IsValid)
        {
            return modelValidationResult.Errors.ToValidatingDetailedBadRequest(
                title: CommonErrorMessages.FailedTo("create listening topic question"),
                detail: CommonErrorMessages.MakeSureAllRequiredFieldsAreProperlyEnter
            );
        }

        var addingResult = await _listeningTopicQuestionsService.AddTopicQuestionsAsync(
            createRequestDto
        );
        if (addingResult.IsFailed || addingResult.ValueOrDefault is null)
        {
            _logger.LogWarning(CommonLoggingMessages.FailedToCreate, nameof(ListeningTopicQuestion));
            return addingResult.Errors.ToDetailedBadRequest();
        }

        var topic = addingResult.Value;

        return Created();
    }
}