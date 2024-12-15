using FluentValidation;
using LearningMate.Core.ConfigurationOptions.AppServer;
using LearningMate.Core.DTOs.ListeningTopicDTOs;
using LearningMate.Core.DTOs.ListeningTopicQuestionDTOs;
using LearningMate.Core.ServiceContracts.ListeningTopicQuestionsServiceContract;
using LearningMate.Core.ServiceContracts.ListeningTopicsServiceContract;
using LearningMate.Domain.IdentityEntities;
using LearningMate.TextToSpeech.ServiceContracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace LearningMate.WebAPI.Controllers.v1.ListeningTopicController;

public partial class ListeningTopicController(
    ILogger<ListeningTopicController> logger,
    UserManager<AppUser> userManager,
    IOptions<MyAppServerConfiguration> myAppServerConfiguration,
    IValidator<ListeningTopicCreateRequestDto> listeningTopicCreateRequestValidator,
    IValidator<ListeningTopicQuestionCreateRequestDto> listeningTopicQuestionCreateRequestValidator,
    IListeningTopicsService listeningTopicsService,
    IListeningTopicQuestionsService listeningTopicQuestionsService,
    ITextToSpeechService textToSpeechService
) : CustomControllerBaseV1(userManager, myAppServerConfiguration)
{
    private readonly ILogger<ListeningTopicController> _logger = logger;
    private readonly IValidator<ListeningTopicCreateRequestDto> _listeningTopicCreateRequestValidator =
        listeningTopicCreateRequestValidator;
    private readonly IValidator<ListeningTopicQuestionCreateRequestDto> _listeningTopicQuestionCreateRequestValidator =
        listeningTopicQuestionCreateRequestValidator;
    private readonly IListeningTopicsService _listeningTopicsService = listeningTopicsService;
    private readonly IListeningTopicQuestionsService _listeningTopicQuestionsService =
        listeningTopicQuestionsService;
    private readonly ITextToSpeechService _textToSpeechService = textToSpeechService;
}
