namespace LearningMate.Core.DTOs.Exam;

public class ExamCreateResponseDto
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public DateTime CreatedAt { get; set; }
}
