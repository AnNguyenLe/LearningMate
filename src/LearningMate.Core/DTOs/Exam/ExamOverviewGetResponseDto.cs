namespace LearningMate.Core.DTOs.Exam;

public class ExamOverviewGetResponseDto
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public DateTime CreatedAt { get; set; }
}
