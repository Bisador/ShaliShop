using Shared.Domain;

namespace ShippingService.Domain.Shipments.DomainEvents;

public record ShipmentDispatched(
    Guid AggregateId,
    Guid OrderId,
    DateTime DispatchedAt,
    string Carrier,
    string TrackingNumber
) : DomainEvent(AggregateId);