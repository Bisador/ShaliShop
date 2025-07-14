using Shared.Domain;

namespace ShippingModule.Domain.Shipments.DomainEvents;

public record ShipmentDeliveryFailed(Guid ShipmentId, int AttemptCount) : DomainEvent;