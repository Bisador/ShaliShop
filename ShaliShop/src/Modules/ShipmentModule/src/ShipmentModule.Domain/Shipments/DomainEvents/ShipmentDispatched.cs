using Shared.Domain;

namespace ShipmentModule.Domain.Shipments.DomainEvents;

public record ShipmentDispatched(
    Guid ShipmentId,
    DateTime DispatchedAt) : DomainEvent;