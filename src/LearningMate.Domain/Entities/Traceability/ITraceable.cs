namespace LearningMate.Domain.Entities.Traceability;

public interface ITraceable
{
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
