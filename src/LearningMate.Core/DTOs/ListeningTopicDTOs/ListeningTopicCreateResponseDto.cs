namespace LearningMate.Core.DTOs.ListeningTopicDTOs;

public class ListeningTopicCreateResponseDto
{
    public Guid Id { get; set; }
    public string? Category { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public double? ScoreBand { get; set; }
    public Guid? ExamId { get; set; }
}
