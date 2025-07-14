using Shared.Domain;

namespace ShippingModule.Domain.Shipments.DomainEvents;

public record ShipmentCreated(
    Guid ShipmentId,
    Guid OrderId) : DomainEvent;