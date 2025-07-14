using Shared.Domain;

namespace ShippingModule.Domain.Shipments.DomainEvents;

public record ShipmentCanceled(
    Guid ShipmentId,
    DateTime CanceledAt) : DomainEvent;