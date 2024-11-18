using FluentResults;
using LearningMate.Core.DTOs.ListeningTopicDTOs;
using LearningMate.Core.ErrorMessages;
using LearningMate.Core.Errors;
using LearningMate.Core.LoggingMessages;
using LearningMate.Domain.Entities;
using LearningMate.Domain.Entities.Listening;
using Microsoft.Extensions.Logging;

namespace LearningMate.Core.Services.ListeningTopicsService;

public partial class ListeningTopicsService
{
    public async Task<Result<ListeningTopicCreateResponseDto>> AddTopicAsync(
        ListeningTopicCreateRequestDto createRequestDto
    )
    {
        var examId = createRequestDto.ExamId;
        if (examId is null)
        {
            return new ProblemDetailsError(
                CommonErrorMessages.FieldCannotBeNull(nameof(ListeningTopic.ExamId))
            );
        }

        var checkExistResult = await _examsRepository.CheckExamExists(examId.Value);

        if (checkExistResult.IsFailed)
        {
            return new ProblemDetailsError(
                CommonErrorMessages.RecordNotFoundWithId(nameof(Exam), examId.Value)
            );
        }

        var topic = _listeningTopicMapper.MapListeningTopicCreateRequestDtoToListeningTopic(
            createRequestDto
        );

        var addResult = await _listeningTopicsRepository.AddTopicAsync(topic);

        if (addResult.IsFailed)
        {
            _logger.LogWarning(CommonLoggingMessages.FailedToCreate, "new listening topic");
            return new ProblemDetailsError(
                CommonErrorMessages.UnexpectedErrorHappenedDuringProcess("adding listening topic")
            );
        }

        var totalAffectedRows = addResult.ValueOrDefault;
        if (totalAffectedRows == 0)
        {
            _logger.LogWarning(CommonLoggingMessages.FailedToCreate, "new listening topic");
            return new ProblemDetailsError(CommonErrorMessages.FailedTo("adding listening topic"));
        }

        return _listeningTopicMapper.MapListeningTopicToListeningTopicCreateResponseDto(topic);
    }
}
