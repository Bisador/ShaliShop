using Microsoft.AspNetCore.Http.HttpResults;
using Shared.Common;

namespace Shared.Presentation;

public static class Extensions
{
    public static ProblemHttpResult Problem(this Result result, string failureTitle = "Operation failed")
    {
        return TypedResults.Problem(new ProblemDetails
        {
            Title = failureTitle,
            Detail = string.Join("; ", result.Errors ?? []),
            Status = StatusCodes.Status400BadRequest
        });
    }
}