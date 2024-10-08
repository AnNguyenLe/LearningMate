using System.ComponentModel.DataAnnotations;

namespace LearningMate.Core.DTOs.Authentication;

public class LogInRequest
{
    [Required(ErrorMessage = "Email cannot be blank.")]
    [EmailAddress(ErrorMessage = "Email should be in a proper email address format.")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Password cannot be blank.")]
    public required string Password { get; set; }
}
