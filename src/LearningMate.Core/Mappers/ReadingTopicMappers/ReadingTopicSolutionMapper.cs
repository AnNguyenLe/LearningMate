using LearningMate.Core.DTOs.ReadingTopicDTOs;
using LearningMate.Domain.Entities.Reading;
using Riok.Mapperly.Abstractions;

namespace LearningMate.Core.Mappers.ReadingTopicMappers;

public partial class ReadingTopicMapper
{
    [MapperIgnoreSource(nameof(ReadingTopic.ScoreBand))]
    [MapperIgnoreSource(nameof(ReadingTopic.Score))]
    [MapperIgnoreSource(nameof(ReadingTopic.ExamId))]
    [MapperIgnoreSource(nameof(ReadingTopic.Exam))]
    public partial ReadingTopicSolutionResponseDto MapReadingTopicToReadingTopicSolutionResponseDto(
        ReadingTopic readingTopic
    );

    [MapperIgnoreSource(nameof(ReadingTopicQuestion.SerializedAnswerOptions))]
    [MapperIgnoreSource(nameof(ReadingTopicQuestion.TopicId))]
    [MapperIgnoreSource(nameof(ReadingTopicQuestion.Topic))]
    private partial ReadingTopicQuestionSolution ReadingTopicQuestionToReadingTopicQuestionSolution(
        ReadingTopicQuestion readingTopicQuestion
    );
}
