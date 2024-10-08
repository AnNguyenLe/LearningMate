using FluentValidation;
using LearningMate.Core.DTOs.Authentication;

namespace LearningMate.Core.Validators.Authentication;

public class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
{
    public ResetPasswordRequestValidator()
    {
        RuleFor(m => m.UserId).NotEmpty().WithMessage("UserId is required.");

        RuleFor(m => m.Token).NotEmpty().WithMessage("Reset password token is required.");

        RuleFor(m => m.NewPassword)
            .NotEmpty()
            .WithMessage("Password is required.")
            .MinimumLength(8)
            .WithMessage("Password must be at least 8 characters.")
            .Must(password => password.Any(char.IsLower))
            .WithMessage("Password must contains lowercase character.")
            .Must(password => password.Any(char.IsUpper))
            .WithMessage("Password must contains uppercase character.")
            .Must(password => password.Any(char.IsDigit))
            .WithMessage("Password must contains digit.")
            .Must(password => password.Any(c => !char.IsLetterOrDigit(c)))
            .WithMessage("Password must contains special character.");

        RuleFor(m => m.ConfirmNewPassword)
            .Equal(m => m.NewPassword)
            .WithMessage("Password and Confirm Password do not match.")
            .NotEmpty()
            .WithMessage("Confirm Password is required.")
            .MinimumLength(8)
            .WithMessage("Confirm Password must be at least 8 characters.")
            .Must(confirmPassword => confirmPassword.Any(char.IsLower))
            .WithMessage("Confirm Password must contains lower case character.")
            .Must(confirmPassword => confirmPassword.Any(char.IsUpper))
            .WithMessage("Confirm Password must contains upper case character.")
            .Must(confirmPassword => confirmPassword.Any(char.IsDigit))
            .WithMessage("Confirm Password must contains digit.")
            .Must(password => password.Any(c => !char.IsLetterOrDigit(c)))
            .WithMessage("Confirm Password must contains special character.");
    }
}
