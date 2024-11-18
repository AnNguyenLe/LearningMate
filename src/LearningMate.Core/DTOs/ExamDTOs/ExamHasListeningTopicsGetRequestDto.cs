using LearningMate.Core.DTOs.ListeningTopicDTOs;

namespace LearningMate.Core.DTOs.ExamDTOs;

public class ExamHasListeningTopicsGetRequestDto
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public List<ListeningTopicTestResponseDto>? ListeningTopics { get; set; }
}
