using LearningMate.Domain.Entities.QuestionTypes.MultipleChoice;

namespace LearningMate.Core.DTOs.ReadingTopicDTOs;

public class ReadingTopicSolutionResponseDto
{
    public Guid Id { get; set; }
    public string? Category { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public List<ReadingTopicQuestionSolution>? Questions { get; set; }
}

public class ReadingTopicQuestionSolution
{
    public Guid Id { get; set; }
    public string? Content { get; set; }
    public List<MultipleChoiceAnswerOption>? AnswerOptions { get; set; }
}
