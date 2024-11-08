using LearningMate.Domain.Entities.QuestionTypes.Rhetoric;

namespace LearningMate.Domain.Entities.Speaking;

public class SpeakingTopicAnswer : IRhetoricalAnswer
{
    public Guid Id { get; set; }
    public string? Answer { get; set; }
    public Guid? QuestionId { get; set; }
    public SpeakingTopic? Question { get; set; }
}
