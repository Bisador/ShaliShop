using System.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Shared.Telemetry;

public class CorrelationIdMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var correlationId = context.Request.Headers["X-Correlation-ID"].FirstOrDefault() ?? Guid.NewGuid().ToString();
        context.Items["CorrelationId"] = correlationId;
        context.Response.Headers["X-Correlation-ID"] = correlationId;

        using var scope = new Activity("Request");
        scope.SetTag("CorrelationId", correlationId);
        scope.Start();
        await next(context);
        scope.Stop();
    }
}