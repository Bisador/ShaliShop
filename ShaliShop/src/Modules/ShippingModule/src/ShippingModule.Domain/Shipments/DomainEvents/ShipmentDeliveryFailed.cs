using Shared.Domain;

namespace ShippingModule.Domain.Shipments.DomainEvents;

public record ShipmentDeliveryFailed(Guid AggregateId, int AttemptCount) : DomainEvent(AggregateId);