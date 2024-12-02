namespace LearningMate.Domain.Entities.Speaking;

public class SpeakingTopic
{
    public Guid Id { get; set; }
    public string? Content { get; set; }
    public List<string>? ResourcesUrl { get; set; }
    public string? SerializedResourcesUrl { get; set; }
    public string? Category { get; set; }
    public double? ScoreBand { get; set; }
    public double? Score { get; set; }
    public Guid? ExamId { get; set; }
    public Exam? Exam { get; set; }
    public List<SpeakingTopicAnswer>? Answers { get; set; }
}
