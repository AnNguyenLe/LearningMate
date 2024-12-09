namespace LearningMate.Core.DTOs.WritingTopicDTOs;

public class WritingTopicSubmitRequestDto
{
    public Guid TopicId { get; set; }
    public string Essay { get; set; } = string.Empty;
}
