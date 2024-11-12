using LearningMate.Domain.Entities.Listening;
using LearningMate.Domain.Entities.Reading;
using LearningMate.Domain.Entities.Speaking;
using LearningMate.Domain.Entities.Writing;
using Riok.Mapperly.Abstractions;

namespace LearningMate.Domain.Entities;

public class Exam
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? SubmissionTime { get; set; }
    public List<ReadingTopic>? ReadingTopics { get; set; }
    public List<ListeningTopic>? ListeningTopics { get; set; }
    public List<WritingTopic>? WritingTopics { get; set; }
    public List<SpeakingTopic>? SpeakingTopics { get; set; }
    public List<ExamineeExamRelationship>? ExamineeExamRelationships { get; set; }
}
