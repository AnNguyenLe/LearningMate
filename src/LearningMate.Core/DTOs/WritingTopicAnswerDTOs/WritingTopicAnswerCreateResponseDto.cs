namespace LearningMate.Core.DTOs.WritingTopicAnswerDTOs;

public class WritingTopicAnswerCreateResponseDto
{
    public Guid Id { get; set; }
    public string? Content { get; set; }
    public Guid? TopicId { get; set; }
}
