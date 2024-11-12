using LearningMate.Core.DTOs.ReadingTopicDTOs;

namespace LearningMate.Core.DTOs.ExamDTOs;

public class ExamHasReadingTopicsGetRequestDto
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public List<ReadingTopicTestRequestDto>? ReadingTopics { get; set; }
}
