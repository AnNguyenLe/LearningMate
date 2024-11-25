using FluentValidation;
using LearningMate.Core.Validators.Authentication;
using LearningMate.Core.Validators.ExamValidator;
using LearningMate.Core.Validators.ListeningTopicQuestionValidator;
using LearningMate.Core.Validators.ListeningTopicValidator;
using LearningMate.Core.Validators.ReadingTopicQuestionValidator;
using LearningMate.Core.Validators.ReadingTopicValidator;

namespace LearningMate.WebAPI.StartupExtensions;

public static class ConfigureValidatorsExtensions
{
    public static IServiceCollection ConfigureValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<ExamCreateRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<ListeningTopicCreateRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<ListeningTopicQuestionCreateRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<ReadingTopicCreateRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<ReadingTopicQuestionCreateRequestValidator>();

        return services;
    }
}
