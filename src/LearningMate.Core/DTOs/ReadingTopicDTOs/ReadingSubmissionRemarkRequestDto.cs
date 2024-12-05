namespace LearningMate.Core.DTOs.ReadingTopicDTOs;

public class ReadingTopicSubmitRequestDto
{
    public Guid Id { get; set; }
    public List<ReadingTopicAnsweredQuestion>? Questions { get; set; }
}

public class ReadingTopicAnsweredQuestion
{
    public Guid Id { get; set; }
    public string? Content { get; set; }
    public List<MultipleChoiceSubmittedOption>? AnswerOptions { get; set; }
}

public class MultipleChoiceSubmittedOption
{
    public Guid Id { get; set; }
    public string? Content { get; set; }
    public bool IsSelected { get; set; }
}
