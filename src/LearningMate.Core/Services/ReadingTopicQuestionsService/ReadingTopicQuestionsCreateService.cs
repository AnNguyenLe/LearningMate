using FluentResults;
using LearningMate.Core.DTOs.ReadingTopicQuestionDTOs;
using LearningMate.Core.ErrorMessages;
using LearningMate.Core.Errors;
using LearningMate.Core.LoggingMessages;
using LearningMate.Domain.Entities.Reading;
using Microsoft.Extensions.Logging;

namespace LearningMate.Core.Services.ReadingTopicQuestionsService;

public partial class ReadingTopicQuestionsService
{
    public async Task<Result<ReadingTopicQuestionCreateResponseDto>> AddTopicQuestionsAsync(
        ReadingTopicQuestionCreateRequestDto createRequestDto
    )
    {
        var topicId = createRequestDto.TopicId;
        if (topicId is null)
        {
            return new ProblemDetailsError(
                CommonErrorMessages.FieldCannotBeNull(nameof(ReadingTopicQuestion.TopicId))
            );
        }

        var checkExistResult = await _readingTopicsRepository.CheckReadingTopicExistsAsync(
            topicId.Value
        );

        if (checkExistResult.IsFailed)
        {
            return new ProblemDetailsError(
                CommonErrorMessages.RecordNotFoundWithId(nameof(ReadingTopic), topicId.Value)
            );
        }

        var topicQuestion =
            _readingTopicQuestionMapper.MapReadingTopicQuestionCreateRequestDtoToReadingTopicQuestion(
                createRequestDto
            );

        var addResult = await _readingTopicQuestionsRepository.AddQuestionAsync(topicQuestion);

        if (addResult.IsFailed)
        {
            _logger.LogWarning(CommonLoggingMessages.FailedToCreate, "new reading topic question");
            return new ProblemDetailsError(
                CommonErrorMessages.UnexpectedErrorHappenedDuringProcess(
                    "adding reading topic question"
                )
            );
        }

        var totalAffectedRows = addResult.ValueOrDefault;
        if (totalAffectedRows == 0)
        {
            _logger.LogWarning(CommonLoggingMessages.FailedToCreate, "new reading topic question");
            return new ProblemDetailsError(
                CommonErrorMessages.FailedTo("adding reading topic question")
            );
        }

        return _readingTopicQuestionMapper.MapReadingTopicQuestionToReadingTopicQuestionCreateResponseDto(
            topicQuestion
        );
    }
}
