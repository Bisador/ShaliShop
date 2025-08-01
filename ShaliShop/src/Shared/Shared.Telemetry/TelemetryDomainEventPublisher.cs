using Microsoft.ApplicationInsights;
using Shared.Application.Events;
using Shared.Domain;

namespace Shared.Telemetry;

public class TelemetryDomainEventPublisher(TelemetryClient telemetry) : IDomainEventPublisher
{
    public Task PublishAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        var properties = new Dictionary<string, string>
        {
            {"EventType", domainEvent.GetType().Name},
            {"AggregateId", domainEvent.AggregateId.ToString()},
            {"Timestamp", DateTime.UtcNow.ToString("o")}
        };

        telemetry.TrackEvent("DomainEventPublished", properties);
        return Task.CompletedTask;
    }
}