namespace LearningMate.Domain.Entities.QuestionTypes.Rhetoric;

public interface IRhetoricalAnswer
{
    public Guid Id { get; set; }
    public Guid? QuestionId { get; set; }
    public string? Answer { get; set; }
}
