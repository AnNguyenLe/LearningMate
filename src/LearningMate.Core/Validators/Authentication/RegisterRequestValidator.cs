using System.Text.RegularExpressions;
using FluentValidation;
using LearningMate.Core.DTOs.Authentication;

namespace LearningMate.Core.Validators.Authentication;

public class RegisterRequestValidator : AbstractValidator<RegistrationRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(model => model.FirstName)
            .Matches("^[a-z ,.'-]+$", RegexOptions.IgnoreCase)
            .WithMessage(
                "Only English characters and these special characters are allowed: comma, space, single quote, dot, and hyphen."
            )
            .NotEmpty()
            .WithMessage("First Name is required.")
            .MinimumLength(2)
            .WithMessage("First Name must at least 2 characters long.");

        RuleFor(model => model.LastName)
            .Matches("^[a-z ,.'-]+$", RegexOptions.IgnoreCase)
            .WithMessage(
                "Only English characters and these special characters are allowed: comma, space, single quote, dot, and hyphen."
            )
            .NotEmpty()
            .WithMessage("Last Name is required.")
            .MinimumLength(2)
            .WithMessage("Last Name must at least 2 characters long.");

        RuleFor(model => model.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid email address format.");

        var today = DateOnly.FromDateTime(DateTime.UtcNow.Date);
        var yearsAgo150 = today.Year - 150;
        RuleFor(model => model.DateOfBirth)
            .NotEmpty()
            .WithMessage("Date of birth is required.")
            .Must(date => date < today)
            .WithMessage("Day of birth cannot be in the future.")
            .Must(date => date.Year >= yearsAgo150)
            .WithMessage($"Year of birth cannot be less than {yearsAgo150}.");

        RuleFor(m => m.Password)
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

        RuleFor(m => m.ConfirmPassword)
            .Equal(m => m.Password)
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
