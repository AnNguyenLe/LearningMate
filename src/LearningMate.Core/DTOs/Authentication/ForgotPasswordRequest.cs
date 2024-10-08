using System.ComponentModel.DataAnnotations;

namespace LearningMate.Core.DTOs.Authentication;

public class ForgotPasswordRequest
{
    [Required(ErrorMessage = "We need your email to process forgot password process.")]
    [EmailAddress(ErrorMessage = "This is not a proper email format.")]
    public required string Email { get; set; }
}
