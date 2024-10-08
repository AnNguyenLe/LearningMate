using System.Net.Mail;
using LearningMate.Core.ServiceContracts.Email;
using Microsoft.Extensions.Logging;

namespace LearningMate.Core.Services.EmailService;

public class EmailSenderService(
    ILogger<EmailSenderService> logger,
    ISmtpClientProvider smtpClientProvider
) : IEmailSenderService
{
    private readonly ILogger<EmailSenderService> _logger = logger;
    private readonly ISmtpClientProvider _smtpClientProvider = smtpClientProvider;

    public async Task SendEmailAsync(string recipientEmail, string subject, string htmlMessage)
    {
        _logger.LogInformation("Start to send email");

        var mailMessage = new MailMessage()
        {
            Subject = subject,
            Body = htmlMessage,
            IsBodyHtml = true
        };

        await _smtpClientProvider.SendMailAsync(recipientEmail, mailMessage);
    }
}
