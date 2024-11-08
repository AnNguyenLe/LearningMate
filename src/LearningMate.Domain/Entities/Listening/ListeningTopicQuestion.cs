using LearningMate.Domain.Entities.QuestionTypes.MultipleChoice;

namespace LearningMate.Domain.Entities.Listening;

public class ListeningTopicQuestion
{
    public Guid Id { get; set; }
    public string? Question { get; set; }
    public string? SerializedAnswerOptions { get; set; } // store as serialized json of List<MultipleChoiceAnswerOption>
    public Guid? TopicId { get; set; }
    public ListeningTopic? Topic { get; set; }
    public List<MultipleChoiceAnswerOption>? AnswerOptions { get; set; }
}
