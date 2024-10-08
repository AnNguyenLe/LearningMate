using LearningMate.Core.ConfigurationOptions.AppServer;
using LearningMate.Core.ServiceContracts.HtmlTemplate;
using Microsoft.Extensions.Options;

namespace LearningMate.Core.Services.HtmlTemplate;

public class EmailHtmlTemplateService(IOptions<MyAppServerConfiguration> myAppServerConfiguration)
    : IEmailHtmlTemplateService
{
    private readonly MyAppServerConfiguration _appServer = myAppServerConfiguration.Value;

    public string GeneratePasswordResetEmailConfirmationTemplate(
        Guid userId,
        string username,
        string confirmationToken
    )
    {
        var confirmationLink =
            $"{_appServer.BaseUrl}/api/v1/account/reset-password?userId={userId}&token={confirmationToken}";

        return $"""
                <!DOCTYPE html>
                <html>
                <head>
                <title>Cyanist Reset Password - Email Confirmation</title>
                </head>
                <body>

                    Hi <strong>{username}</strong>,

                    <p>You can find below the link to reset your password.</p>

                    {confirmationLink}

                </body>
                </html>
            """;
    }

    public string GenerateRegisterEmailConfirmationTemplate(
        Guid userId,
        string username,
        string confirmationToken
    )
    {
        var confirmationLink =
            $"{_appServer.BaseUrl}/api/v1/account/confirm-email?userId={userId}&token={confirmationToken}";

        return $"""
                <!DOCTYPE html>
                <html>
                <head>
                <title>Cyanist Registration - Email Confirmation</title>
                </head>
                <body>

                    Hi <strong>{username}</strong>,

                    <p>We are so happy that you choose to experience with us.</p>

                    <p>Please click the confirmation button below to signing in our product. And we'll see you there!</p>

                    <a href="{confirmationLink}" style="color:white;background-color:blue;font-size:20px;text-align:center;">
                        <button>Confirm email and sign in</button>
                    </a>

                    {confirmationLink}

                </body>
                </html>
            """;
    }
}
