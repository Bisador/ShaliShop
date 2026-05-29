using Shared.Domain;

namespace ShippingService.Domain.Shipments.DomainEvents;

public record ShipmentDelivered(
    Guid AggregateId,
    DateTime DeliveredAt) : DomainEvent(AggregateId);