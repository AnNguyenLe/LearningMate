using System.Web;
using FluentResults;
using LearningMate.Core.Common.Authorization;
using LearningMate.Core.DTOs.Authentication;
using LearningMate.Core.Errors;
using LearningMate.Core.ServiceContracts.Authentication;
using LearningMate.Core.ServiceContracts.Email;
using LearningMate.Core.ServiceContracts.HtmlTemplate;
using LearningMate.Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace LearningMate.Core.Services.Authentication;

public class AuthenticationService(
    ILogger<AuthenticationService> logger,
    UserManager<AppUser> userManager,
    IEmailSenderService emailSenderService,
    IEmailHtmlTemplateService emailHtmlTemplateService,
    SignInManager<AppUser> signInManager,
    IJwtService jwtService
) : IAuthenticationService
{
    private readonly ILogger<AuthenticationService> _logger = logger;
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly IEmailSenderService _emailSenderService = emailSenderService;
    private readonly IEmailHtmlTemplateService _emailHtmlTemplateService = emailHtmlTemplateService;
    private readonly SignInManager<AppUser> _signInManager = signInManager;
    private readonly IJwtService _jwtService = jwtService;

    public async Task<Result<AuthenticationResponse>> RegisterNewUserAsync(
        RegistrationRequest request
    )
    {
        var existingUser = await _userManager.FindByEmailAsync(request.Email);

        if (existingUser is not null)
        {
            if (await _userManager.IsEmailConfirmedAsync(existingUser))
            {
                return new ProblemDetailsError(
                    title: "Existing account.",
                    detail: "Please signing in using this email and your password."
                );
            }

            return new ProblemDetailsError(
                title: "Action required: Please confirm your email to verify you are the real user.",
                detail: "This email had been registered but not yet to be confirmed. Please go to your mailnox and click the confirm button."
            );
        }

        var user = new AppUser()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            UserName = request.Email,
            DateOfBirth = request.DateOfBirth
        };

        var createUserResult = await _userManager.CreateAsync(user, request.Password);

        if (!createUserResult.Succeeded)
        {
            _logger.LogWarning("Failed to register new user.");

            return new ProblemDetailsError(
                title: "Failed to register new user.",
                detail: "Make sure all the required fields are properly entered."
            );
        }

        // Enable this feature if you want to send register confirmation email
        // await SendRegisterConfirmationEmailAsync(user);

        await _signInManager.SignInAsync(user, false);

        var userRoles = await _userManager.GetRolesAsync(user);

        var accessTokenData = _jwtService.GenerateAccessToken(user, [.. userRoles]);

        return new AuthenticationResponse
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email!,
            AccessToken = accessTokenData.AccessToken,
            ExpiryOfAccessToken = accessTokenData.ExpiresAt,
            RefreshToken = string.Empty, //TODO: RefreshToken Needed!
            IsAdmin = userRoles.HasRole(AppUserRoles.ADMIN),
        };
    }

    public async Task<Result<AuthenticationResponse>> ConfirmEmailUserRegistrationAsync(
        EmailConfirmationRequest request
    )
    {
        var userId = request.UserId;
        var token = request.Token;

        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
        {
            _logger.LogWarning("Cannot find user with id {userId}.", userId);
            return new ProblemDetailsError(
                title: "User does not exist.",
                detail: "Cannot find user."
            );
        }

        var confirmationToken = HttpUtility.UrlDecode(token.Replace("-", "%"));

        var emailConfirmationResult = await _userManager.ConfirmEmailAsync(user, confirmationToken);

        if (!emailConfirmationResult.Succeeded)
        {
            _logger.LogWarning("Failed to validate email for user with id {userId}.", userId);
            return new ProblemDetailsError(
                title: "Failed to validate email.",
                detail: "Failed to validate email for this user."
            );
        }

        await _signInManager.SignInAsync(user, false);

        var userRoles = await _userManager.GetRolesAsync(user);

        var accessTokenData = _jwtService.GenerateAccessToken(user, [.. userRoles]);

        return new AuthenticationResponse
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email!,
            AccessToken = accessTokenData.AccessToken,
            ExpiryOfAccessToken = accessTokenData.ExpiresAt,
            RefreshToken = string.Empty, //TODO: RefreshToken Needed!
            IsAdmin = userRoles.HasRole(AppUserRoles.ADMIN)
        };
    }

    public async Task<Result<AuthenticationResponse>> LogInAsync(LogInRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
        {
            return new ProblemDetailsError("User not found.");
        }

        var signinResult = await _signInManager.PasswordSignInAsync(
            user,
            request.Password,
            false,
            false
        );

        if (!signinResult.Succeeded)
        {
            return new ProblemDetailsError(
                title: "Failed to login.",
                detail: "Make sure that you are entering the correct email and password."
            );
        }

        var userRoles = await _userManager.GetRolesAsync(user);

        var accessTokenData = _jwtService.GenerateAccessToken(user, [.. userRoles]);

        return new AuthenticationResponse()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = request.Email,
            AccessToken = accessTokenData.AccessToken,
            ExpiryOfAccessToken = accessTokenData.ExpiresAt,
            RefreshToken = string.Empty, //TODO: RefreshToken Needed!
            IsAdmin = userRoles.HasRole(AppUserRoles.ADMIN)
        };
    }

    public async Task<Result> LogOutAsync()
    {
        await _signInManager.SignOutAsync();
        return Result.Ok();
    }

    public async Task<Result> ForgotPasswordAsync(ForgotPasswordRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
        {
            return new ProblemDetailsError(title: "Invalid request.");
        }

        await SendPasswordResetConfirmationEmailAsync(user);

        return Result.Ok();
    }

    public async Task<Result> ResetPasswordAsync(ResetPasswordRequest request)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);

        if (user is null)
        {
            return new ProblemDetailsError(title: "User not found", detail: "Invalid User ID.");
        }

        var confirmationToken = HttpUtility.UrlDecode(request.Token.Replace("-", "%"));

        var result = await _userManager.ResetPasswordAsync(
            user,
            confirmationToken,
            request.NewPassword
        );

        if (!result.Succeeded)
        {
            return new ProblemDetailsError(
                title: "Failed to reset password.",
                detail: "Make sure the token is not expired. You must use the original values and do not alter any of those."
            );
        }

        return Result.Ok();
    }

    private async Task SendRegisterConfirmationEmailAsync(AppUser user)
    {
        var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        var confirmationTemplate =
            _emailHtmlTemplateService.GenerateRegisterEmailConfirmationTemplate(
                user.Id,
                $"{user.FirstName} {user.LastName}",
                HttpUtility.UrlEncode(confirmationToken).Replace("%", "-")
            );

        await _emailSenderService.SendEmailAsync(
            user.Email!,
            "Cyanist Registration - Email Confirmation",
            confirmationTemplate
        );
        _logger.LogInformation("Registration Email Confirmation sent.");
    }

    private async Task SendPasswordResetConfirmationEmailAsync(AppUser user)
    {
        var confirmationToken = await _userManager.GeneratePasswordResetTokenAsync(user);

        var confirmationTemplate =
            _emailHtmlTemplateService.GeneratePasswordResetEmailConfirmationTemplate(
                user.Id,
                $"{user.FirstName} {user.LastName}",
                HttpUtility.UrlEncode(confirmationToken).Replace("%", "-")
            );

        await _emailSenderService.SendEmailAsync(
            user.Email!,
            "Cyanist - Reset Password",
            confirmationTemplate
        );
        _logger.LogInformation("Reset Password Email Confirmation sent.");
    }
}
