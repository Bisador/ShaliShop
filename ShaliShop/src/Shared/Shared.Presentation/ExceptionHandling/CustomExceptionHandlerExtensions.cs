namespace Shared.Presentation.ExceptionHandling;

public static class CustomExceptionHandlerExtensions
{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app) =>
        app.UseMiddleware<CustomExceptionHandlerMiddleware>();
}