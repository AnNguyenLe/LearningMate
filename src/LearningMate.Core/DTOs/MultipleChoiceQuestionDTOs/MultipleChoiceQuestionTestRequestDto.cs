using LearningMate.Core.DTOs.MultipleChoiceAnswerOptionDTOs;
using LearningMate.Domain.Entities.QuestionTypes.MultipleChoice;

namespace LearningMate.Core.DTOs.MultipleChoiceQuestionDTOs;

public class MultipleChoiceQuestionTestRequestDto
{
    public Guid Id { get; set; }
    public string? Content { get; set; }
    public List<MultipleChoiceAnswerOptionTestRequestDto>? AnswerOptions { get; set; }
}
