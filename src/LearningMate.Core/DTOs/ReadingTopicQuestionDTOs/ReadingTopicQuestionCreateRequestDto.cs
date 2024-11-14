using LearningMate.Core.DTOs.MultipleChoiceAnswerOptionDTOs;

namespace LearningMate.Core.DTOs.ReadingTopicQuestionDTOs;

public class ReadingTopicQuestionCreateRequestDto
{
    public string? Content { get; set; }
    public List<MultipleChoiceAnswerOptionCreateRequestDto>? AnswerOptions { get; set; }
    public Guid? TopicId { get; set; }
}
