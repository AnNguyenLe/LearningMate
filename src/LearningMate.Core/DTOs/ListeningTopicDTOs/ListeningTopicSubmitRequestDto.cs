namespace LearningMate.Core.DTOs.ListeningTopicDTOs;

public class ListeningTopicSubmitRequestDto
{
    public Guid Id { get; set; }
    public List<ListeningTopicAnsweredQuestion>? Questions { get; set; }
}

public class ListeningTopicAnsweredQuestion
{
    public Guid Id { get; set; }
    public string? Content { get; set; }
    public List<ListeningMultipleChoiceSubmittedOption>? AnswerOptions { get; set; }
}

public class ListeningMultipleChoiceSubmittedOption
{
    public Guid Id { get; set; }
    public string? Content { get; set; }
    public bool IsSelected { get; set; }
}
