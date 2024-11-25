namespace LearningMate.Core.DTOs.ListeningTopicDTOs;

public class ListeningTopicCreateRequestDto
{
    public string? Category { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public double? ScoreBand { get; set; }
    public Guid? ExamId { get; set; }
}
