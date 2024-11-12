using FluentValidation;
using LearningMate.Core.Validators.Authentication;
using LearningMate.Core.Validators.ExamValidator;

namespace LearningMate.WebAPI.StartupExtensions;

public static class ConfigureValidatorsExtensions
{
    public static IServiceCollection ConfigureValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<ExamCreateRequestValidator>();

        return services;
    }
}
