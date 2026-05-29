using Shared.Domain;

namespace ShippingService.Domain.Shipments.DomainEvents;

public record ShipmentDeliveryFailed(Guid AggregateId, int AttemptCount) : DomainEvent(AggregateId);