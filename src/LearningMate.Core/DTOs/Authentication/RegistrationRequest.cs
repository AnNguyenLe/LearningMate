using LearningMate.Domain.IdentityEntities;

namespace LearningMate.Core.DTOs.Authentication;

public class RegistrationRequest
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public required string Password { get; set; }
    public required string ConfirmPassword { get; set; }

    public AppUser ToAppUser() =>
        new()
        {
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
            UserName = Email,
            DateOfBirth = DateOfBirth
        };
}
