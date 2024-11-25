using LearningMate.Domain.Entities.QuestionTypes.MultipleChoice;

namespace LearningMate.Core.DTOs.ListeningTopicQuestionDTOs;

public class ListeningTopicQuestionCreateResponseDto
{
    public Guid Id { get; set; }
    public string? Content { get; set; }
    public Guid? TopicId { get; set; }
    public List<MultipleChoiceAnswerOption>? AnswerOptions { get; set; }
}
