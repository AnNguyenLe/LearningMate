using FluentValidation;
using LearningMate.Core.ConfigurationOptions.AppServer;
using LearningMate.Core.DTOs.ReadingTopicDTOs;
using LearningMate.Core.DTOs.ReadingTopicQuestionDTOs;
using LearningMate.Core.ServiceContracts.ReadingTopicQuestionsServiceContract;
using LearningMate.Core.ServiceContracts.ReadingTopicsServiceContract;
using LearningMate.Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace LearningMate.WebAPI.Controllers.v1.ReadingTopicController;

public partial class ReadingTopicController(
    ILogger<ReadingTopicController> logger,
    UserManager<AppUser> userManager,
    IOptions<MyAppServerConfiguration> myAppServerConfiguration,
    IValidator<ReadingTopicCreateRequestDto> readingTopicCreateRequestValidator,
    IValidator<ReadingTopicQuestionCreateRequestDto> readingTopicQuestionCreateRequestValidator,
    IReadingTopicsService readingTopicsService,
    IReadingTopicQuestionsService readingTopicQuestionsService
) : CustomControllerBaseV1(userManager, myAppServerConfiguration)
{
    private readonly ILogger<ReadingTopicController> _logger = logger;
    private readonly IValidator<ReadingTopicCreateRequestDto> _readingTopicCreateRequestValidator =
        readingTopicCreateRequestValidator;
    private readonly IValidator<ReadingTopicQuestionCreateRequestDto> _readingTopicQuestionCreateRequestValidator =
        readingTopicQuestionCreateRequestValidator;
    private readonly IReadingTopicsService _readingTopicsService = readingTopicsService;
    private readonly IReadingTopicQuestionsService _readingTopicQuestionsService =
        readingTopicQuestionsService;
}
