using LearningMate.Core.DTOs.Exam;
using LearningMate.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace LearningMate.Core.Mappers.ExamMappers;

[Mapper]
public partial class ExamMapper
{
    [MapperIgnoreTarget(nameof(Exam.Id))]
    [MapperIgnoreTarget(nameof(Exam.CreatedAt))]
    [MapperIgnoreTarget(nameof(Exam.StartTime))]
    [MapperIgnoreTarget(nameof(Exam.SubmissionTime))]
    [MapperIgnoreTarget(nameof(Exam.ExamineeExamRelationships))]
    [MapperIgnoreTarget(nameof(Exam.ReadingTopics))]
    [MapperIgnoreTarget(nameof(Exam.ListeningTopics))]
    [MapperIgnoreTarget(nameof(Exam.WritingTopics))]
    [MapperIgnoreTarget(nameof(Exam.SpeakingTopics))]
    private partial Exam ExamCreateRequestDtoToExam(ExamCreateRequestDto dto);

    [UserMapping(Default = false)]
    public Exam MapExamCreateRequestDtoToExam(ExamCreateRequestDto dto)
    {
        var exam = ExamCreateRequestDtoToExam(dto);

        exam.Id = Guid.NewGuid();
        exam.CreatedAt = DateTime.UtcNow;

        return exam;
    }

    [MapperIgnoreSource(nameof(Exam.StartTime))]
    [MapperIgnoreSource(nameof(Exam.SubmissionTime))]
    [MapperIgnoreSource(nameof(Exam.ExamineeExamRelationships))]
    [MapperIgnoreSource(nameof(Exam.ReadingTopics))]
    [MapperIgnoreSource(nameof(Exam.ListeningTopics))]
    [MapperIgnoreSource(nameof(Exam.WritingTopics))]
    [MapperIgnoreSource(nameof(Exam.SpeakingTopics))]
    public partial ExamCreateResponseDto MapExamToExamCreateResponseDto(Exam exam);

    [MapperIgnoreSource(nameof(Exam.StartTime))]
    [MapperIgnoreSource(nameof(Exam.SubmissionTime))]
    [MapperIgnoreSource(nameof(Exam.ExamineeExamRelationships))]
    [MapperIgnoreSource(nameof(Exam.ReadingTopics))]
    [MapperIgnoreSource(nameof(Exam.ListeningTopics))]
    [MapperIgnoreSource(nameof(Exam.WritingTopics))]
    [MapperIgnoreSource(nameof(Exam.SpeakingTopics))]
    public partial ExamOverviewGetResponseDto MapExamToExamOverviewGetResponseDto(Exam exam);
}
