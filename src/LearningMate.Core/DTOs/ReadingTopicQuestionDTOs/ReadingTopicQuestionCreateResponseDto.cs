using LearningMate.Domain.Entities.QuestionTypes.MultipleChoice;

namespace LearningMate.Core.DTOs.ReadingTopicQuestionDTOs;

public class ReadingTopicQuestionCreateResponseDto
{
    public Guid Id { get; set; }
    public string? Content { get; set; }
    public Guid? TopicId { get; set; }
    public List<MultipleChoiceAnswerOption>? AnswerOptions { get; set; }
}
