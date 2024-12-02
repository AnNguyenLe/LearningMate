using FluentResults;
using LearningMate.Core.DTOs.WritingTopicDTOs;
using LearningMate.Core.ErrorMessages;
using LearningMate.Core.Errors;
using LearningMate.Core.LoggingMessages;
using LearningMate.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace LearningMate.Core.Services.WritingTopicsService;

public partial class WritingTopicsService
{
    public async Task<Result<WritingTopicCreateResponseDto>> AddTopicAsync(
        WritingTopicCreateRequestDto createRequestDto
    )
    {
        var examId = createRequestDto.ExamId;
        if (examId is null)
        {
            return new ProblemDetailsError(
                CommonErrorMessages.FieldCannotBeNull(nameof(WritingTopicCreateRequestDto.ExamId))
            );
        }

        var checkExistResult = await _examsRepository.CheckExamExists(examId.Value);

        if (checkExistResult.IsFailed)
        {
            return new ProblemDetailsError(
                CommonErrorMessages.RecordNotFoundWithId(nameof(Exam), examId.Value)
            );
        }

        var topic = _writingTopicMapper.MapWritingTopicCreateRequestDtoToWritingTopic(
            createRequestDto
        );

        var addResult = await _writingTopicsRepository.AddTopicAsync(topic);

        if (addResult.IsFailed)
        {
            _logger.LogWarning(CommonLoggingMessages.FailedToCreate, "new writing topic");
            return new ProblemDetailsError(
                CommonErrorMessages.UnexpectedErrorHappenedDuringProcess("adding writing topic")
            );
        }

        var totalAffectedRows = addResult.ValueOrDefault;
        if (totalAffectedRows == 0)
        {
            _logger.LogWarning(CommonLoggingMessages.FailedToCreate, "new reading topic");
            return new ProblemDetailsError(CommonErrorMessages.FailedTo("adding reading topic"));
        }

        return _writingTopicMapper.MapWritingTopicToWritingTopicCreateResponseDto(topic);
    }
}
