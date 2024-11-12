using FluentValidation;
using LearningMate.Core.Mappers.ExamMappers;
using LearningMate.Core.ServiceContracts.Authentication;
using LearningMate.Core.ServiceContracts.Email;
using LearningMate.Core.ServiceContracts.ExamsServiceContract;
using LearningMate.Core.ServiceContracts.HtmlTemplate;
using LearningMate.Core.Services.Authentication;
using LearningMate.Core.Services.EmailService;
using LearningMate.Core.Services.ExamsService;
using LearningMate.Core.Services.HtmlTemplate;
using LearningMate.Core.Validators.Authentication;
using LearningMate.Domain.RepositoryContracts;
using LearningMate.Infrastructure.Data;
using LearningMate.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace LearningMate.WebAPI.StartupExtensions;

public static class DependencyInjectionServicesExtension
{
    public static IServiceCollection DependencyInjectionServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.TryAddScoped<IJwtService, JwtService>();
        services.TryAddScoped<IAuthenticationService, AuthenticationService>();
        services.TryAddScoped<IEmailHtmlTemplateService, EmailHtmlTemplateService>();
        services.TryAddScoped<IEmailSenderService, EmailSenderService>();
        services.TryAddScoped<ISmtpClientProvider, SmtpClientProvider>();

        services.TryAddSingleton<IDbConnectionFactory>(_ => new NpgsqlDbConnectionFactory(
            configuration.GetConnectionString("Default")!
        ));

        services.AddScoped<IExamsService, ExamsService>();
        services.AddScoped<IExamsRepository, ExamsRepository>();

        services.TryAddSingleton<ExamMapper>();

        return services;
    }
}
