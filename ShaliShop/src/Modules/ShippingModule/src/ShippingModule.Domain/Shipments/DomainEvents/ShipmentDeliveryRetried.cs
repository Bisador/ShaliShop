using Shared.Domain;

namespace ShippingModule.Domain.Shipments.DomainEvents;

public record ShipmentDeliveryRetried(Guid ShipmentId, DateTime RetriedAt) : DomainEvent;