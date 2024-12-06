using LearningMate.Core.DTOs.ListeningTopicDTOs;
using LearningMate.Domain.Entities.Listening;
using Riok.Mapperly.Abstractions;
namespace LearningMate.Core.Mappers.ListeningTopicMappers;
public partial class ListeningTopicMapper
{
    [MapperIgnoreSource(nameof(ListeningTopic.ScoreBand))]
    [MapperIgnoreSource(nameof(ListeningTopic.Score))]
    [MapperIgnoreSource(nameof(ListeningTopic.ExamId))]
    [MapperIgnoreSource(nameof(ListeningTopic.Exam))]
    public partial ListeningTopicSolutionResponseDto MapListeningTopicToListeningTopicSolutionResponseDto(
        ListeningTopic listeningTopic
    );
    [MapperIgnoreSource(nameof(ListeningTopicQuestion.SerializedAnswerOptions))]
    [MapperIgnoreSource(nameof(ListeningTopicQuestion.TopicId))]
    [MapperIgnoreSource(nameof(ListeningTopicQuestion.Topic))]
    private partial ListeningTopicQuestionSolution ListeningTopicQuestionToListeningTopicQuestionSolution(
        ListeningTopicQuestion listeningTopicQuestion
    );
}