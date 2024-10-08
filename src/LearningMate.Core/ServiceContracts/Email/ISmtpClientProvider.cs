using System.Net.Mail;

namespace LearningMate.Core.ServiceContracts.Email;

public interface ISmtpClientProvider : IDisposable
{
    Task SendMailAsync(string recipientEmail, MailMessage mailMessage);
}
