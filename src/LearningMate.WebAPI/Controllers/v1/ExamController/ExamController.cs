using FluentValidation;
using LearningMate.Core.ConfigurationOptions.AppServer;
using LearningMate.Core.DTOs.Exam;
using LearningMate.Core.ServiceContracts.ExamsServiceContract;
using LearningMate.Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace LearningMate.WebAPI.Controllers.v1.ExamController;

public partial class ExamController(
    UserManager<AppUser> userManager,
    IOptions<MyAppServerConfiguration> myAppServerConfiguration,
    ILogger<ExamController> logger,
    IExamsService examsService,
    IValidator<ExamCreateRequestDto> examCreateRequestValidator
) : CustomControllerBaseV1(userManager, myAppServerConfiguration)
{
    private readonly ILogger<ExamController> _logger = logger;
    private readonly IExamsService _examsService = examsService;
    private readonly IValidator<ExamCreateRequestDto> _examCreateRequestValidator =
        examCreateRequestValidator;
}
