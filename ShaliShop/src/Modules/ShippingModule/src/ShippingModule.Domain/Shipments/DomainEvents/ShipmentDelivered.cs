using Shared.Domain;

namespace ShippingModule.Domain.Shipments.DomainEvents;

public record ShipmentDelivered(
    Guid ShipmentId,
    DateTime DeliveredAt) : DomainEvent;