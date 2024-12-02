namespace LearningMate.Domain.Entities.Speaking;

public class SpeakingTopicAnswer
{
    public Guid Id { get; set; }
    public string? Content { get; set; }
    public Guid? TopicId { get; set; }
    public SpeakingTopic? Topic { get; set; }
}
