using LearningMate.Core.DTOs.ExamDTOs;
using LearningMate.Core.DTOs.WritingTopicDTOs;
using LearningMate.Domain.Entities;
using LearningMate.Domain.Entities.Writing;
using Riok.Mapperly.Abstractions;

namespace LearningMate.Core.Mappers.ExamMappers;

public partial class ExamMapper
{
    [MapperIgnoreSource(nameof(Exam.CreatedAt))]
    [MapperIgnoreSource(nameof(Exam.StartTime))]
    [MapperIgnoreSource(nameof(Exam.SubmissionTime))]
    [MapperIgnoreSource(nameof(Exam.ReadingTopics))]
    [MapperIgnoreSource(nameof(Exam.ListeningTopics))]
    [MapperIgnoreSource(nameof(Exam.SpeakingTopics))]
    [MapperIgnoreSource(nameof(Exam.ExamineeExamRelationships))]
    public partial ExamHasWritingTopicsGetRequestDto MapExamToExamHasWritingTopicsGetRequestDto(
        Exam exam
    );

    [MapperIgnoreSource(nameof(WritingTopic.ExamId))]
    [MapperIgnoreSource(nameof(WritingTopic.Score))]
    [MapperIgnoreSource(nameof(WritingTopic.Exam))]
    [MapperIgnoreSource(nameof(WritingTopic.SerializedResourcesUrl))]
    [MapperIgnoreSource(nameof(WritingTopic.Answers))]
    private partial WritingTopicTestResponseDto WritingTopicToWritingTopicTestResponseDto(
        WritingTopic writingTopic
    );
}
