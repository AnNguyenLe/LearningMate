using System.ComponentModel.DataAnnotations;

namespace LearningMate.Core.DTOs.Authentication;

public class EmailConfirmationRequest
{
    [Required(ErrorMessage = "{0} cannot be blank.")]
    public required string UserId { get; set; }

    [Required(ErrorMessage = "{0} cannot be blank.")]
    public required string Token { get; set; }
}
