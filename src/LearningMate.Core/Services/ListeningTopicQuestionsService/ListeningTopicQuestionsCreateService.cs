using FluentResults;
using LearningMate.Core.DTOs.ListeningTopicQuestionDTOs;
using LearningMate.Core.ErrorMessages;
using LearningMate.Core.Errors;
using LearningMate.Core.LoggingMessages;
using LearningMate.Domain.Entities.Listening;
using Microsoft.Extensions.Logging;

namespace LearningMate.Core.Services.ListeningTopicQuestionsService;

public partial class ListeningTopicQuestionsService
{
    public async Task<Result<ListeningTopicQuestionCreateResponseDto>> AddTopicQuestionsAsync(
        ListeningTopicQuestionCreateRequestDto createRequestDto
    )
    {
        var topicId = createRequestDto.TopicId;
        if (topicId is null)
        {
            return new ProblemDetailsError(
                CommonErrorMessages.FieldCannotBeNull(nameof(ListeningTopicQuestion.TopicId))
            );
        }

        var checkExistResult = await _listeningTopicsRepository.CheckListeningTopicExistsAsync(
            topicId.Value
        );

        if (checkExistResult.IsFailed)
        {
            return new ProblemDetailsError(
                CommonErrorMessages.RecordNotFoundWithId(nameof(ListeningTopic), topicId.Value)
            );
        }

        var topicQuestion =
            _listeningTopicQuestionMapper.MapListeningTopicQuestionCreateRequestDtoToListeningTopicQuestion(
                createRequestDto
            );

        var addResult = await _listeningTopicQuestionsRepository.AddQuestionAsync(topicQuestion);

        if (addResult.IsFailed)
        {
            _logger.LogWarning(CommonLoggingMessages.FailedToCreate, "new listening topic question");
            return new ProblemDetailsError(
                CommonErrorMessages.UnexpectedErrorHappenedDuringProcess(
                    "adding listening topic question"
                )
            );
        }

        var totalAffectedRows = addResult.ValueOrDefault;
        if (totalAffectedRows == 0)
        {
            _logger.LogWarning(CommonLoggingMessages.FailedToCreate, "new listening topic question");
            return new ProblemDetailsError(
                CommonErrorMessages.FailedTo("adding listening topic question")
            );
        }

        return _listeningTopicQuestionMapper.MapListeningTopicQuestionToListeningTopicQuestionCreateResponseDto(
            topicQuestion
        );
    }
}
