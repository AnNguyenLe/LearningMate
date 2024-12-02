using FluentValidation;
using LearningMate.Core.Validators.Authentication;
using LearningMate.Core.Validators.ExamValidator;
using LearningMate.Core.Validators.ListeningTopicQuestionValidator;
using LearningMate.Core.Validators.ListeningTopicValidator;
using LearningMate.Core.Validators.ReadingTopicQuestionValidator;
using LearningMate.Core.Validators.ReadingTopicValidator;
using LearningMate.Core.Validators.SpeakingTopicAnswerValidator;
using LearningMate.Core.Validators.SpeakingTopicValidator;
using LearningMate.Core.Validators.WritingTopicAnswerValidator;
using LearningMate.Core.Validators.WritingTopicValidator;

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
        services.AddValidatorsFromAssemblyContaining<WritingTopicCreateRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<WritingTopicAnswerCreateRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<SpeakingTopicCreateRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<SpeakingTopicAnswerCreateRequestValidator>();

        return services;
    }
}
