namespace LearningMate.Core.DTOs.SpeakingTopicDTOs;
public class SpeakingTopicSubmitRequestDto
{
    public Guid TopicId { get; set; }
    public string Essay { get; set; } = string.Empty;
}