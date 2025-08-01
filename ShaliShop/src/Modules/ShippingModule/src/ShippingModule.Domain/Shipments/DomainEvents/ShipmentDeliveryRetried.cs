using Shared.Domain;

namespace ShippingModule.Domain.Shipments.DomainEvents;

public record ShipmentDeliveryRetried(Guid AggregateId, DateTime RetriedAt) : DomainEvent(AggregateId);