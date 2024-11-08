using LearningMate.Domain.Entities.QuestionTypes.Rhetoric;

namespace LearningMate.Domain.Entities.Writing;

public class WritingTopicAnswer : IRhetoricalAnswer
{
    public Guid Id { get; set; }
    public string? Answer { get; set; }
    public Guid? QuestionId { get; set; }
    public WritingTopic? Question { get; set; }
}
