using LearningMate.Core.DTOs.SpeakingTopicDTOs;

namespace LearningMate.Core.DTOs.ExamDTOs;

public class ExamHasSpeakingTopicsGetRequestDto
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public List<SpeakingTopicTestResponseDto>? SpeakingTopics { get; set; }
}
