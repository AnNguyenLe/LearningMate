namespace LearningMate.Domain.Entities.Reading;

public class ReadingTopic
{
    public Guid Id { get; set; }
    public string? Category { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public double? ScoreBand { get; set; }
    public double? Score { get; set; }
    public Guid? ExamId { get; set; }
    public Exam? Exam { get; set; }
    public List<ReadingTopicQuestion>? Questions { get; set; }
}
