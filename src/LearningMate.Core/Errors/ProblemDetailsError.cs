using FluentResults;

namespace LearningMate.Core.Errors;

public class ProblemDetailsError : Error
{
    public string Title { get; init; }
    public string Detail { get; init; }

    public ProblemDetailsError()
    {
        Title = string.Empty;
        Detail = string.Empty;
    }

    public ProblemDetailsError(string title)
    {
        Title = title;
        Detail = title;
    }

    public ProblemDetailsError(string title, string detail)
    {
        Title = title;
        Detail = detail;
    }
}
