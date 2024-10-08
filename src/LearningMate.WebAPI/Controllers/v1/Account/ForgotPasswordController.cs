using Asp.Versioning;
using LearningMate.Core.Common.ExtensionMethods;
using LearningMate.Core.DTOs.Authentication;
using LearningMate.Core.ServiceContracts.Authentication;
using LearningMate.WebAPI.Filters.ActionFilterAttributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningMate.WebAPI.Controllers.v1.Account
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/account/forgot-password")]
    public class ForgotPasswordController(IAuthenticationService authenticationService)
        : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService = authenticationService;

        [HttpPost(Name = "ForgotPassword")]
        [AllowAnonymous]
        [ModelBindingFailureFormatFilter]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest request)
        {
            var result = await _authenticationService.ForgotPasswordAsync(request);

            if (result.IsFailed)
            {
                return result.Errors.ToDetailedBadRequest();
            }

            return Ok();
        }
    }
}
