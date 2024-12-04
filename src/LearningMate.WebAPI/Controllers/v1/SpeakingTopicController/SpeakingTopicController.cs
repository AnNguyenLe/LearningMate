using FluentValidation;
using LearningMate.Core.ConfigurationOptions.AppServer;
using LearningMate.Core.DTOs.SpeakingTopicAnswerDTOs;
using LearningMate.Core.DTOs.SpeakingTopicDTOs;
using LearningMate.Core.ServiceContracts.SpeakingTopicAnswersServiceContract;
using LearningMate.Core.ServiceContracts.SpeakingTopicsServiceContract;
using LearningMate.Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
namespace LearningMate.WebAPI.Controllers.v1.SpeakingTopicController;
public partial class SpeakingTopicController(
    ILogger<SpeakingTopicController> logger,
    UserManager<AppUser> userManager,
    IOptions<MyAppServerConfiguration> myAppServerConfiguration,
    IValidator<SpeakingTopicCreateRequestDto> speakingTopicCreateRequestValidator,
    IValidator<SpeakingTopicAnswerCreateRequestDto> speakingTopicAnswerCreateRequestValidator,
    ISpeakingTopicsService speakingTopicsService,
    ISpeakingTopicAnswersService speakingTopicAnswersService
) : CustomControllerBaseV1(userManager, myAppServerConfiguration)
{
    private readonly ILogger<SpeakingTopicController> _logger = logger;
    private readonly IValidator<SpeakingTopicCreateRequestDto> _speakingTopicCreateRequestValidator =
        speakingTopicCreateRequestValidator;
    private readonly IValidator<SpeakingTopicAnswerCreateRequestDto> _speakingTopicAnswerCreateRequestValidator =
        speakingTopicAnswerCreateRequestValidator;
    private readonly ISpeakingTopicsService _speakingTopicsService = speakingTopicsService;
    private readonly ISpeakingTopicAnswersService _speakingTopicAnswersService =
        speakingTopicAnswersService;
}