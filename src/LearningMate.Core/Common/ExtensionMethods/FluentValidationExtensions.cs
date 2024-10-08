using System.Net;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace LearningMate.Core.Common.ExtensionMethods;

public static class FluentValidationExtensions
{
    public static ObjectResult ToValidatingDetailedBadRequest(
        this IEnumerable<ValidationFailure> validationErrors,
        string title,
        string detail
    )
    {
        var modelErrors = new Dictionary<string, string[]>();
        if (validationErrors.Any())
        {
            modelErrors = validationErrors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());
        }

        return new ObjectResult(
            new ValidationProblemDetails()
            {
                Title = title,
                Detail = detail,
                Status = (int)HttpStatusCode.BadRequest,
                Errors = modelErrors
            }
        );
    }
}
