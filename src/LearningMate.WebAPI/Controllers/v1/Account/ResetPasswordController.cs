using LearningMate.WebAPI.Filters.ActionFilterAttributes;
using LearningMate.Core.Common.ExtensionMethods;
using LearningMate.Core.DTOs.Authentication;
using LearningMate.Core.ServiceContracts.Authentication;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningMate.WebAPI.Controllers.v1.Account
{
    [ApiController]
    [Route("api/v1/account/reset-password")]
    [AllowAnonymous]
    public class ResetPasswordController(
        IValidator<ResetPasswordRequest> validator,
        IAuthenticationService authenticationService
    ) : ControllerBase
    {
        private readonly IValidator<ResetPasswordRequest> _validator = validator;
        private readonly IAuthenticationService _authenticationService = authenticationService;

        [HttpPost]
        [ModelBindingFailureFormatFilter]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            var modelValidationResult = _validator.Validate(request);

            if (!modelValidationResult.IsValid)
            {
                return modelValidationResult.Errors.ToValidatingDetailedBadRequest(
                    title: "Failed to reset password.",
                    detail: "Make sure all the required fields is properly entered."
                );
            }

            var result = await _authenticationService.ResetPasswordAsync(request);

            if (result.IsFailed)
            {
                return result.Errors.ToDetailedBadRequest();
            }

            return Ok();
        }
    }
}
