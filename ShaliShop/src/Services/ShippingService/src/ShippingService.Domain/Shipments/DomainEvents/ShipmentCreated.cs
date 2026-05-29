using Shared.Domain;

namespace ShippingService.Domain.Shipments.DomainEvents;

public record ShipmentCreated(
    Guid AggregateId,
    Guid OrderId) : DomainEvent(AggregateId);