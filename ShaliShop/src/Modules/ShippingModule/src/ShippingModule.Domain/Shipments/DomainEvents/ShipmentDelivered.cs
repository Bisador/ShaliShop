using Shared.Domain;

namespace ShippingModule.Domain.Shipments.DomainEvents;

public record ShipmentDelivered(
    Guid AggregateId,
    DateTime DeliveredAt) : DomainEvent(AggregateId);