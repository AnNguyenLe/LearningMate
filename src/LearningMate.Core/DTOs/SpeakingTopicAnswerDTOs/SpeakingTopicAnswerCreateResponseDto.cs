namespace LearningMate.Core.DTOs.SpeakingTopicAnswerDTOs;
public class SpeakingTopicAnswerCreateResponseDto
{
    public Guid Id { get; set; }
    public string? Content { get; set; }
    public Guid? TopicId { get; set; }
}