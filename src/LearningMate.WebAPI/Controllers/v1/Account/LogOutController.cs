using Asp.Versioning;
using LearningMate.Core.Common.ExtensionMethods;
using LearningMate.Core.ServiceContracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace LearningMate.WebAPI.Controllers.v1.Account;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/account/logout")]
public class LogOutController(IAuthenticationService authenticationService) : ControllerBase
{
    private readonly IAuthenticationService _authenticationService = authenticationService;

    [HttpGet]
    public async Task<IActionResult> LogOut()
    {
        var result = await _authenticationService.LogOutAsync();
        if (result.IsFailed)
        {
            return result.Errors.ToDetailedBadRequest();
        }
        return Ok();
    }
}
