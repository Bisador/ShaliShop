using Microsoft.ApplicationInsights;
using Shared.Domain;
using Shared.Eventing.Abstraction;

namespace Shared.Telemetry;

public class TelemetryDomainEventPublisherDecorator(
    IDomainEventPublisher inner,
    TelemetryClient telemetry) : IDomainEventPublisher
{
    public async Task PublishAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        telemetry.TrackEvent("DomainEventPublished", new Dictionary<string, string>
        {
            {"EventType", domainEvent.GetType().Name},
            {"AggregateId", domainEvent.AggregateId.ToString()},
            {"Timestamp", DateTime.UtcNow.ToString("o")}
        });

        await inner.PublishAsync(domainEvent, cancellationToken);
    }
}