namespace LearningMate.Domain.Entities.QuestionTypes.MultipleChoice;

public interface IMultipleChoiceQuestion
{
    public Guid Id { get; set; }
    public string? Content { get; set; }
    public string? SerializedAnswerOptions { get; set; } // store as serialized json of List<MultipleChoiceAnswerOption>
    public Guid? TopicId { get; set; }
    public List<MultipleChoiceAnswerOption>? AnswerOptions { get; set; }
}
