using LearningMate.Domain.Entities.QuestionTypes.MultipleChoice;

namespace LearningMate.Domain.Entities.Reading;

public class ReadingTopicQuestion : IMultipleChoiceQuestion
{
    public Guid Id { get; set; }
    public string? Question { get; set; }
    public string? SerializedAnswerOptions { get; set; } // store as serialized json of List<MultipleChoiceAnswerOption>
    public Guid? TopicId { get; set; }
    public ReadingTopic? Topic { get; set; }
    public List<MultipleChoiceAnswerOption>? AnswerOptions { get; set; }
}
