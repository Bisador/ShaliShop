using System.Diagnostics;
using MediatR;
using Microsoft.ApplicationInsights;

namespace Shared.Telemetry;

public class TelemetryCommandHandlerDecorator<TRequest, TResponse>(IRequestHandler<TRequest, TResponse> inner, TelemetryClient telemetry)
    : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
        var stopwatch = Stopwatch.StartNew();
        var requestName = typeof(TRequest).Name;
        try
        {
            var response = await inner.Handle(request, cancellationToken);
            stopwatch.Stop();

            telemetry.TrackMetric($"{requestName}_ExecutionTime", stopwatch.ElapsedMilliseconds);
            return response;
        }
        catch (Exception ex)
        {
            telemetry.TrackException(ex, new Dictionary<string, string>
            {
                {"Command", requestName}
            });
            throw;
        }
    }
}