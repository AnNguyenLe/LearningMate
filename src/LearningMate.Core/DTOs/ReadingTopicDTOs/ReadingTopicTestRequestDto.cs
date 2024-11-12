using LearningMate.Core.DTOs.MultipleChoiceQuestionDTOs;

namespace LearningMate.Core.DTOs.ReadingTopicDTOs;

public class ReadingTopicTestRequestDto
{
    public Guid Id { get; set; }
    public string? Category { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public double? ScoreBand { get; set; }
    public List<MultipleChoiceQuestionTestRequestDto>? Questions { get; set; }
}
