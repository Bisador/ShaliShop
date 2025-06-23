using Shared.Domain;

namespace ShipmentModule.Domain.Shipments.DomainEvents;

public record ShipmentDeliveryFailed(Guid ShipmentId, int AttemptCount) : DomainEvent;