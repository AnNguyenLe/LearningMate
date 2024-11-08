namespace LearningMate.Domain.Entities.QuestionTypes.Rhetoric;

public interface IRhetoricalQuestion
{
    public Guid Id { get; set; }
    public string? Question { get; set; }
    public string? ResourcesUrl { get; set; }
}
