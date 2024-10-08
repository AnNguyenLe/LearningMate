using System.Net;
using System.Net.Mail;
using LearningMate.Core.ConfigurationOptions.Smtp;
using LearningMate.Core.ServiceContracts.Email;
using Microsoft.Extensions.Options;

namespace LearningMate.Core.Services.EmailService;

public class SmtpClientProvider(IOptions<SmtpConfiguration> smtpConfiguration) : ISmtpClientProvider
{
    private readonly SmtpConfiguration _smtpConfiguration = smtpConfiguration.Value;
    private readonly SmtpClient emailClient = CreateSmtpClient(
        smtpConfiguration.Value.Host,
        smtpConfiguration.Value.Port
    );

    private static SmtpClient CreateSmtpClient(string host, int port)
    {
        return new SmtpClient(host, port);
    }

    public async Task SendMailAsync(string recipientEmail, MailMessage mailMessage)
    {
        mailMessage.From = new MailAddress(
            _smtpConfiguration.SenderEmail,
            _smtpConfiguration.SenderName
        );

        mailMessage.To.Add(recipientEmail);

        emailClient.Credentials = new NetworkCredential(
            _smtpConfiguration.Username,
            _smtpConfiguration.Password
        );

        await emailClient.SendMailAsync(mailMessage);

        Dispose();
    }

    public void Dispose()
    {
        emailClient.Dispose();
        GC.SuppressFinalize(this);
    }
}
