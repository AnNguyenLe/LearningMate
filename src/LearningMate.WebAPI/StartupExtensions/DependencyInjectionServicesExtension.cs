using LearningMate.Core.Mappers.ExamMappers;
using LearningMate.Core.Mappers.ListeningTopicMappers;
using LearningMate.Core.Mappers.ListeningTopicQuestionMappers;
using LearningMate.Core.Mappers.ReadingTopicMappers;
using LearningMate.Core.Mappers.ReadingTopicQuestionMappers;
using LearningMate.Core.Mappers.SpeakingTopicAnswerMappers;
using LearningMate.Core.Mappers.SpeakingTopicMappers;
using LearningMate.Core.Mappers.WritingTopicAnswerMappers;
using LearningMate.Core.Mappers.WritingTopicMappers;
using LearningMate.Core.ServiceContracts.Authentication;
using LearningMate.Core.ServiceContracts.Email;
using LearningMate.Core.ServiceContracts.ExamsServiceContract;
using LearningMate.Core.ServiceContracts.HtmlTemplate;
using LearningMate.Core.ServiceContracts.ListeningTopicQuestionsServiceContract;
using LearningMate.Core.ServiceContracts.ListeningTopicsServiceContract;
using LearningMate.Core.ServiceContracts.ReadingTopicQuestionsServiceContract;
using LearningMate.Core.ServiceContracts.ReadingTopicsServiceContract;
using LearningMate.Core.ServiceContracts.SpeakingTopicAnswerServiceContract;
using LearningMate.Core.ServiceContracts.SpeakingTopicsServiceContract;
using LearningMate.Core.ServiceContracts.WritingTopicAnswersServiceContract;
using LearningMate.Core.ServiceContracts.WritingTopicsServiceContract;
using LearningMate.Core.Services.Authentication;
using LearningMate.Core.Services.EmailService;
using LearningMate.Core.Services.ExamsService;
using LearningMate.Core.Services.HtmlTemplate;
using LearningMate.Core.Services.ListeningTopicQuestionsService;
using LearningMate.Core.Services.ListeningTopicsService;
using LearningMate.Core.Services.ReadingTopicQuestionsService;
using LearningMate.Core.Services.ReadingTopicsService;
using LearningMate.Core.Services.SpeakingTopicAnswersService;
using LearningMate.Core.Services.SpeakingTopicsService;
using LearningMate.Core.Services.WritingTopicAnswersService;
using LearningMate.Core.Services.WritingTopicsService;
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

        services.AddScoped<IReadingTopicsService, ReadingTopicsService>();
        services.AddScoped<IReadingTopicQuestionsService, ReadingTopicQuestionsService>();
        services.AddScoped<IReadingTopicsRepository, ReadingTopicsRepository>();
        services.AddScoped<IReadingTopicQuestionsRepository, ReadingTopicQuestionsRepository>();

        services.AddScoped<IListeningTopicsService, ListeningTopicsService>();
        services.AddScoped<IListeningTopicQuestionsService, ListeningTopicQuestionsService>();
        services.AddScoped<IListeningTopicsRepository, ListeningTopicsRepository>();
        services.AddScoped<IListeningTopicQuestionsRepository, ListeningTopicQuestionsRepository>();

        services.AddScoped<IWritingTopicsService, WritingTopicsService>();
        services.AddScoped<IWritingTopicAnswersService, WritingTopicAnswersService>();
        services.AddScoped<IWritingTopicsRepository, WritingTopicsRepository>();
        services.AddScoped<IWritingTopicAnswersRepository, WritingTopicAnswersRepository>();

        services.AddScoped<ISpeakingTopicsService, SpeakingTopicsService>();
        services.AddScoped<ISpeakingTopicAnswersService, SpeakingTopicAnswersService>();
        services.AddScoped<ISpeakingTopicsRepository, SpeakingTopicsRepository>();
        services.AddScoped<ISpeakingTopicAnswersRepository, SpeakingTopicAnswersRepository>();

        services.TryAddSingleton<ExamMapper>();
        services.TryAddSingleton<ReadingTopicMapper>();
        services.TryAddSingleton<ReadingTopicQuestionMapper>();
        services.TryAddSingleton<ListeningTopicMapper>();
        services.TryAddSingleton<ListeningTopicQuestionMapper>();
        services.TryAddSingleton<SpeakingTopicMapper>();
        services.TryAddSingleton<SpeakingTopicAnswerMapper>();
        services.TryAddSingleton<WritingTopicMapper>();
        services.TryAddSingleton<WritingTopicAnswerMapper>();

        return services;
    }
}
