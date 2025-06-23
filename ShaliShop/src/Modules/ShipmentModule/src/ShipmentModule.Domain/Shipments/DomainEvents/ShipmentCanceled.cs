using Shared.Domain;

namespace ShipmentModule.Domain.Shipments.DomainEvents;

public record ShipmentCanceled(
    Guid ShipmentId,
    DateTime CanceledAt) : DomainEvent;