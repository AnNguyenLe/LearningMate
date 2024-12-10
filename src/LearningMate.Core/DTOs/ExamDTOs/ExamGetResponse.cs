namespace LearningMate.Core.DTOs.ExamDTOs;

public class ExamGetResponseDto
{
    public Guid Id { get; set; }
    public string? Title { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
