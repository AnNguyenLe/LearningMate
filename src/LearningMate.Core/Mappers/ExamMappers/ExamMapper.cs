using System.Text.Json;
using LearningMate.Core.DTOs.ExamDTOs;
using LearningMate.Core.DTOs.MultipleChoiceAnswerOptionDTOs;
using LearningMate.Core.DTOs.MultipleChoiceQuestionDTOs;
using LearningMate.Core.DTOs.ReadingTopicDTOs;
using LearningMate.Domain.Entities;
using LearningMate.Domain.Entities.QuestionTypes.MultipleChoice;
using LearningMate.Domain.Entities.Reading;
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

    [MapperIgnoreSource(nameof(Exam.CreatedAt))]
    [MapperIgnoreSource(nameof(Exam.StartTime))]
    [MapperIgnoreSource(nameof(Exam.SubmissionTime))]
    [MapperIgnoreSource(nameof(Exam.ListeningTopics))]
    [MapperIgnoreSource(nameof(Exam.WritingTopics))]
    [MapperIgnoreSource(nameof(Exam.SpeakingTopics))]
    [MapperIgnoreSource(nameof(Exam.ExamineeExamRelationships))]
    private partial ExamHasReadingTopicsGetRequestDto ExamToExamHasReadingTopicsGetRequestDto(
        Exam exam
    );

    [MapperIgnoreSource(nameof(MultipleChoiceAnswerOption.IsCorrectAnswer))]
    private partial MultipleChoiceAnswerOptionTestRequestDto MultipleChoiceAnswerOptionToMultipleChoiceAnswerOptionTestRequestDto(
        MultipleChoiceAnswerOption option
    );

    [MapperIgnoreSource(nameof(ReadingTopicQuestion.SerializedAnswerOptions))]
    [MapperIgnoreSource(nameof(ReadingTopicQuestion.TopicId))]
    [MapperIgnoreSource(nameof(ReadingTopicQuestion.Topic))]
    private partial MultipleChoiceQuestionTestRequestDto ReadingTopicQuestionToMultipleChoiceQuestionTestRequestDto(
        ReadingTopicQuestion question
    );

    [MapperIgnoreSource(nameof(ReadingTopic.ExamId))]
    [MapperIgnoreSource(nameof(ReadingTopic.Exam))]
    [MapperIgnoreSource(nameof(ReadingTopic.Score))]
    private partial ReadingTopicTestResponseDto ReadingTopicToReadingTopicTestResponseDto(
        ReadingTopic readingTopic
    );

    [UserMapping(Default = false)]
    public ExamHasReadingTopicsGetRequestDto MapExamToExamHasReadingTopicsGetRequestDto(Exam exam)
    {
        if (exam.ReadingTopics is null || exam.ReadingTopics.Count == 0)
        {
            exam.ReadingTopics = [];
        }

        exam.ReadingTopics.ForEach(topic =>
        {
            if (topic.Questions is null || topic.Questions.Count == 0)
            {
                topic.Questions = [];
                return;
            }

            topic.Questions.ForEach(question =>
            {
                if (
                    question.SerializedAnswerOptions is null
                    || string.IsNullOrWhiteSpace(question.SerializedAnswerOptions)
                )
                {
                    question.AnswerOptions = [];
                    return;
                }

                List<MultipleChoiceAnswerOption>? parsedJson;
                try
                {
                    parsedJson = JsonSerializer.Deserialize<List<MultipleChoiceAnswerOption>>(
                        question.SerializedAnswerOptions
                    );
                }
                catch (JsonException)
                {
                    question.AnswerOptions = [];
                    return;
                }

                if (parsedJson is null)
                {
                    question.AnswerOptions = [];
                    return;
                }
                question.AnswerOptions = parsedJson;
            });
        });

        var dto = ExamToExamHasReadingTopicsGetRequestDto(exam);
        dto.ReadingTopics = exam
            .ReadingTopics.Select(ReadingTopicToReadingTopicTestResponseDto)
            .ToList();
        return dto;
    }
}
