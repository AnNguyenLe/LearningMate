using Asp.Versioning;
using LearningMate.Core.Common.ExtensionMethods;
using LearningMate.Core.DTOs.Authentication;
using LearningMate.Core.ServiceContracts.Authentication;
using LearningMate.WebAPI.Filters.ActionFilterAttributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningMate.WebAPI.Controllers.v1.Account;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/account/login")]
public class LogInController(IAuthenticationService authenticationService) : ControllerBase
{
    private readonly IAuthenticationService _authenticationService = authenticationService;

    [HttpPost]
    [AllowAnonymous]
    [ModelBindingFailureFormatFilter]
    public async Task<ActionResult<AuthenticationResponse>> LogIn(LogInRequest loginRequest)
    {
        var loginResult = await _authenticationService.LogInAsync(loginRequest);

        if (loginResult.IsFailed)
        {
            return loginResult.Errors.ToDetailedBadRequest();
        }

        return loginResult.ValueOrDefault;
    }
}
