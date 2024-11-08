using LearningMate.Domain.Entities.QuestionTypes.Rhetoric;

namespace LearningMate.Domain.Entities.Writing;

public class WritingTopic : IRhetoricalQuestion
{
    public Guid Id { get; set; }
    public string? Question { get; set; }
    public string? ResourcesUrl { get; set; }
    public string? Category { get; set; }
    public double? ScoreBand { get; set; }
    public double? Score { get; set; }
    public Guid? ExamId { get; set; }
    public Exam? Exam { get; set; }
    public List<WritingTopicAnswer>? Answers { get; set; }
}
