using LearningMate.Core.DTOs.ExamDTOs;
using LearningMate.Core.DTOs.SpeakingTopicDTOs;
using LearningMate.Domain.Entities;
using LearningMate.Domain.Entities.Speaking;
using Riok.Mapperly.Abstractions;

namespace LearningMate.Core.Mappers.ExamMappers;

public partial class ExamMapper
{
    [MapperIgnoreSource(nameof(Exam.CreatedAt))]
    [MapperIgnoreSource(nameof(Exam.StartTime))]
    [MapperIgnoreSource(nameof(Exam.SubmissionTime))]
    [MapperIgnoreSource(nameof(Exam.ReadingTopics))]
    [MapperIgnoreSource(nameof(Exam.ListeningTopics))]
    [MapperIgnoreSource(nameof(Exam.WritingTopics))]
    [MapperIgnoreSource(nameof(Exam.ExamineeExamRelationships))]
    public partial ExamHasSpeakingTopicsGetRequestDto MapExamToExamHasSpeakingTopicsGetRequestDto(
        Exam exam
    );

    [MapperIgnoreSource(nameof(SpeakingTopic.ExamId))]
    [MapperIgnoreSource(nameof(SpeakingTopic.Score))]
    [MapperIgnoreSource(nameof(SpeakingTopic.Exam))]
    [MapperIgnoreSource(nameof(SpeakingTopic.SerializedResourcesUrl))]
    [MapperIgnoreSource(nameof(SpeakingTopic.Answers))]
    private partial SpeakingTopicTestResponseDto SpeakingTopicToSpeakingTopicTestResponseDto(
        SpeakingTopic speakingTopic
    );
}
