using System.Net.Mail;
using FluentValidation;
using LearningMate.Core.ServiceContracts.Authentication;
using LearningMate.Core.ServiceContracts.Email;
using LearningMate.Core.ServiceContracts.HtmlTemplate;
using LearningMate.Core.Services.Authentication;
using LearningMate.Core.Services.EmailService;
using LearningMate.Core.Services.HtmlTemplate;
using LearningMate.Core.Validators.Authentication;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace LearningMate.WebAPI.StartupExtensions;

public static class DependencyInjectionServicesExtension
{
    public static IServiceCollection DependencyInjectionServices(this IServiceCollection services)
    {
        services.TryAddScoped<IJwtService, JwtService>();
        services.TryAddScoped<IAuthenticationService, AuthenticationService>();
        services.TryAddScoped<IEmailHtmlTemplateService, EmailHtmlTemplateService>();
        services.TryAddScoped<IEmailSenderService, EmailSenderService>();
        services.TryAddScoped<ISmtpClientProvider, SmtpClientProvider>();

        services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();

        return services;
    }
}
