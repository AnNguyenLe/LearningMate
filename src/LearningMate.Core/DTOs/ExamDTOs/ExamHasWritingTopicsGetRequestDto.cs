using LearningMate.Core.DTOs.WritingTopicDTOs;

namespace LearningMate.Core.DTOs.ExamDTOs;

public class ExamHasWritingTopicsGetRequestDto
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public List<WritingTopicTestResponseDto>? WritingTopics { get; set; }
}
