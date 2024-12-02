namespace LearningMate.Domain.Entities.Writing;

public class WritingTopicAnswer
{
    public Guid Id { get; set; }
    public string? Content { get; set; }
    public Guid? TopicId { get; set; }
    public WritingTopic? Topic { get; set; }
}
