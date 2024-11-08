using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace LearningMate.WebAPI.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class CustomControllerBase : ControllerBase
{
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
}
