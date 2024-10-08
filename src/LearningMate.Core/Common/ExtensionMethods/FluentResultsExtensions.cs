using System.Net;
using FluentResults;
using LearningMate.Core.Errors;
using Microsoft.AspNetCore.Mvc;

namespace LearningMate.Core.Common.ExtensionMethods;

public static class FluentResultsExtensions
{
    public static ObjectResult ToDetailedBadRequest(this IEnumerable<IError> errors)
    {
        var error = errors.OfType<ProblemDetailsError>().FirstOrDefault();
        if (error is null)
        {
            return new ObjectResult(
                new ProblemDetails() { Status = (int)HttpStatusCode.BadRequest }
            );
        }
        return new ObjectResult(
            new ProblemDetails()
            {
                Title = error.Title,
                Detail = error.Detail,
                Status = (int)HttpStatusCode.BadRequest
            }
        );
    }
}
