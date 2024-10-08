using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LearningMate.WebAPI.Filters.ActionFilterAttributes;

public class ModelBindingFailureFormatFilter : ActionFilterAttribute
{
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        base.OnResultExecuting(context);
        if (context.Result is BadRequestObjectResult badRequestObjectResult)
        {
            if (badRequestObjectResult.Value is ValidationProblemDetails)
            {
                context.Result = new UnprocessableEntityObjectResult(
                    new ProblemDetails()
                    {
                        Title = "Invalid request.",
                        Detail =
                            "Please make sure that all the keys and values are provided properly."
                    }
                );
            }
        }
    }
}
