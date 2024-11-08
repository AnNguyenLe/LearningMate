using LearningMate.Domain.Entities.Traceability;
using LearningMate.Domain.IdentityEntities;

namespace LearningMate.Domain.Entities;

public class ExamineeExamRelationship : ITraceable
{
    public Guid Id { get; set; }
    public double? OverallScore { get; set; }
    public Guid? ExamineeId { get; set; }
    public Guid? ExamId { get; set; }
    public AppUser? Examinee { get; set; }
    public Exam? Exam { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
