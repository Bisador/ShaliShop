using Shared.Domain;

namespace ShipmentModule.Domain.Shipments.DomainEvents;

public record ShipmentDelivered(
    Guid ShipmentId,
    DateTime DeliveredAt) : DomainEvent;