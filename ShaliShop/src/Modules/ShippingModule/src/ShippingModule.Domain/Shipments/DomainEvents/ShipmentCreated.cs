using Shared.Domain;

namespace ShippingModule.Domain.Shipments.DomainEvents;

public record ShipmentCreated(
    Guid AggregateId,
    Guid OrderId) : DomainEvent(AggregateId);