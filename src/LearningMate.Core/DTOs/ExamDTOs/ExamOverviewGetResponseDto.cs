namespace LearningMate.Core.DTOs.ExamDTOs;

public class ExamOverviewGetResponseDto
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public DateTime CreatedAt { get; set; }
}
