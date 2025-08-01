using Microsoft.Extensions.Logging;
using Shared.Domain;

namespace Shared.Presentation.ExceptionHandling;

public class CustomExceptionHandlerMiddleware(RequestDelegate next, ILogger<CustomExceptionHandlerMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var statusCode = StatusCodes.Status500InternalServerError;
            var title = "An unexpected error occurred";
            var detail = ex.Message;
            var errors = new Dictionary<string, string[]>();
            
            switch (ex)
            {
                case ValidationException validationEx:
                    statusCode = StatusCodes.Status400BadRequest;
                    title = "Validation failed";
                    errors = validationEx.Errors
                        .GroupBy(e => e.PropertyName)
                        .ToDictionary(
                            g => g.Key,
                            g => g.Select(e => e.ErrorMessage).ToArray()
                        );
                    break;

                case DomainException domainEx:
                    statusCode = StatusCodes.Status400BadRequest;
                    title = "Domain error";
                    detail = domainEx.Message;
                    break;

                case UnauthorizedAccessException:
                    statusCode = StatusCodes.Status401Unauthorized;
                    title = "Unauthorized";
                    break;

                case NotImplementedException:
                    statusCode = StatusCodes.Status501NotImplemented;
                    title = "Feature not implemented";
                    break;
                
            }
            
            logger.LogError(ex, "Exception at {Path} with status {StatusCode}", context.Request.Path, statusCode); 
 
            var problemDetails = new ProblemDetails
            {
                Title = title,
                Detail = detail,
                Status = statusCode,
                Instance = context.Request.Path
            };
            
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/problem+json";

            if (errors.Count == 0)
            {
                await context.Response.WriteAsJsonAsync(problemDetails);
            }
            else
            {
                var validationProblem = new ValidationProblemDetails(errors)
                {
                    Title = title,
                    Detail = "See error details",
                    Status = statusCode,
                    Instance = context.Request.Path
                };

                await context.Response.WriteAsJsonAsync(validationProblem);
            }
        }
    }
}