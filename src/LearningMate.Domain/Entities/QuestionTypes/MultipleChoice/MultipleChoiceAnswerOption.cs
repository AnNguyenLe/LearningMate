namespace LearningMate.Domain.Entities.QuestionTypes.MultipleChoice;

public class MultipleChoiceAnswerOption
{
    public Guid Id { get; set; }
    public string? Content { get; set; }
    public bool IsCorrectAnswer { get; set; }
}
