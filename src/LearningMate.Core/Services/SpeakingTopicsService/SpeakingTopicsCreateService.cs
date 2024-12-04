using FluentResults;
using LearningMate.Core.DTOs.SpeakingTopicDTOs;
using LearningMate.Core.ErrorMessages;
using LearningMate.Core.Errors;
using LearningMate.Core.LoggingMessages;
using LearningMate.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace LearningMate.Core.Services.SpeakingTopicsService;

public partial class SpeakingTopicsService
{
    public async Task<Result<SpeakingTopicCreateResponseDto>> AddTopicAsync(
        SpeakingTopicCreateRequestDto createRequestDto
    )
    {
        var examId = createRequestDto.ExamId;
        if (examId is null)
        {
            return new ProblemDetailsError(
                CommonErrorMessages.FieldCannotBeNull(nameof(SpeakingTopicCreateRequestDto.ExamId))
            );
        }
        var checkExistResult = await _examsRepository.CheckExamExists(examId.Value);
        if (checkExistResult.IsFailed)
        {
            return new ProblemDetailsError(
                CommonErrorMessages.RecordNotFoundWithId(nameof(Exam), examId.Value)
            );
        }
        var topic = _speakingTopicMapper.MapSpeakingTopicCreateRequestDtoToSpeakingTopic(
            createRequestDto
        );
        var addResult = await _speakingTopicsRepository.AddTopicAsync(topic);
        if (addResult.IsFailed)
        {
            _logger.LogWarning(CommonLoggingMessages.FailedToCreate, "new speaking topic");
            return new ProblemDetailsError(
                CommonErrorMessages.UnexpectedErrorHappenedDuringProcess("adding speaking topic")
            );
        }
        var totalAffectedRows = addResult.ValueOrDefault;
        if (totalAffectedRows == 0)
        {
            _logger.LogWarning(CommonLoggingMessages.FailedToCreate, "new speaking topic");
            return new ProblemDetailsError(CommonErrorMessages.FailedTo("adding speaking topic"));
        }

        return _speakingTopicMapper.MapSpeakingTopicToSpeakingTopicCreateResponseDto(topic);
    }
}
