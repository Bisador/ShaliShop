using Shared.Domain;

namespace ShipmentModule.Domain.Shipments.DomainEvents;

public record ShipmentDeliveryRetried(Guid ShipmentId, DateTime RetriedAt) : DomainEvent;