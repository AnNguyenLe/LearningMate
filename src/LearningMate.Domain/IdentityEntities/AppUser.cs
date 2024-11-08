using LearningMate.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace LearningMate.Domain.IdentityEntities;

public class AppUser : IdentityUser<Guid>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string? RefreshTokenHash { get; set; }
    public DateTime RefreshTokenExpiryDateTime { get; set; }
    public List<ExamineeExamRelationship>? ExamineeExamRelationships { get; set; }
}
