using System.Net;
using Asp.Versioning;
using FluentResults;
using LearningMate.Core.ConfigurationOptions.AppServer;
using LearningMate.Core.ErrorMessages;
using LearningMate.Core.Errors;
using LearningMate.Domain.IdentityEntities;
using LearningMate.WebAPI.Filters.ActionFilterAttributes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LearningMate.WebAPI.Controllers.v1;

[ApiController]
[ModelBindingFailureFormatFilter]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}")]
public class CustomControllerBaseV1(
    UserManager<AppUser> userManager,
    IOptions<MyAppServerConfiguration> myAppServerConfiguration
) : ControllerBase
{
    protected readonly UserManager<AppUser> _userManager = userManager;
    private readonly MyAppServerConfiguration _app = myAppServerConfiguration.Value;

    protected ObjectResult BadRequestProblemDetails(string title)
    {
        return new ObjectResult(
            new ProblemDetails()
            {
                Title = title,
                Detail = title,
                Status = (int)HttpStatusCode.BadRequest
            }
        );
    }

    protected ObjectResult BadRequestProblemDetails(string title, string detail)
    {
        return new ObjectResult(
            new ProblemDetails()
            {
                Title = title,
                Detail = detail,
                Status = (int)HttpStatusCode.BadRequest
            }
        );
    }

    protected Result<Guid> TryGetUserId()
    {
        var error = new ProblemDetailsError(CommonErrorMessages.UnableToIdentifyUser);
        if (User is null || User.Identity is null)
        {
            return error;
        }
        var userId = _userManager.GetUserId(User);

        return userId is null || !Guid.TryParse(userId, out var parsedUserId)
            ? error
            : parsedUserId;
    }
}
