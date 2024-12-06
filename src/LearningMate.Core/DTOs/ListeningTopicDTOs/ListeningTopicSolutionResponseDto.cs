using LearningMate.Domain.Entities.QuestionTypes.MultipleChoice;
namespace LearningMate.Core.DTOs.ListeningTopicDTOs;
public class ListeningTopicSolutionResponseDto
{
    public Guid Id { get; set; }
    public string? Category { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public List<ListeningTopicQuestionSolution>? Questions { get; set; }
}
public class ListeningTopicQuestionSolution
{
    public Guid Id { get; set; }
    public string? Content { get; set; }
    public List<MultipleChoiceAnswerOption>? AnswerOptions { get; set; }
}