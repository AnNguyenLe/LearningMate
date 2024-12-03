namespace LearningMate.Core.DTOs.SpeakingTopicDTOs;
public class SpeakingTopicTestResponseDto
{
    public Guid Id { get; set; }
    public string? Content { get; set; }
    public List<string>? ResourcesUrl { get; set; }
    public string? Category { get; set; }
    public double? ScoreBand { get; set; }
}