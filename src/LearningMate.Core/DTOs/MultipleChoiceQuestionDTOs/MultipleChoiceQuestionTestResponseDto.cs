using LearningMate.Core.DTOs.MultipleChoiceAnswerOptionDTOs;

namespace LearningMate.Core.DTOs.MultipleChoiceQuestionDTOs;

public class MultipleChoiceQuestionTestResponseDto
{
    public Guid Id { get; set; }
    public string? Content { get; set; }
    public List<MultipleChoiceAnswerOptionTestResponseDto>? AnswerOptions { get; set; }
}
