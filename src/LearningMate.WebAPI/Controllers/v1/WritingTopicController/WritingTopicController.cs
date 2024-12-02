using FluentValidation;
using LearningMate.Core.ConfigurationOptions.AppServer;
using LearningMate.Core.DTOs.WritingTopicAnswerDTOs;
using LearningMate.Core.DTOs.WritingTopicDTOs;
using LearningMate.Core.ServiceContracts.WritingTopicAnswersServiceContract;
using LearningMate.Core.ServiceContracts.WritingTopicsServiceContract;
using LearningMate.Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace LearningMate.WebAPI.Controllers.v1.WritingTopicController;

public partial class WritingTopicController(
    ILogger<WritingTopicController> logger,
    UserManager<AppUser> userManager,
    IOptions<MyAppServerConfiguration> myAppServerConfiguration,
    IValidator<WritingTopicCreateRequestDto> writingTopicCreateRequestValidator,
    IValidator<WritingTopicAnswerCreateRequestDto> writingTopicAnswerCreateRequestValidator,
    IWritingTopicsService writingTopicsService,
    IWritingTopicAnswersService writingTopicAnswersService
) : CustomControllerBaseV1(userManager, myAppServerConfiguration)
{
    private readonly ILogger<WritingTopicController> _logger = logger;
    private readonly IValidator<WritingTopicCreateRequestDto> _writingTopicCreateRequestValidator =
        writingTopicCreateRequestValidator;
    private readonly IValidator<WritingTopicAnswerCreateRequestDto> _writingTopicAnswerCreateRequestValidator =
        writingTopicAnswerCreateRequestValidator;
    private readonly IWritingTopicsService _writingTopicsService = writingTopicsService;
    private readonly IWritingTopicAnswersService _writingTopicAnswersService =
        writingTopicAnswersService;
}
