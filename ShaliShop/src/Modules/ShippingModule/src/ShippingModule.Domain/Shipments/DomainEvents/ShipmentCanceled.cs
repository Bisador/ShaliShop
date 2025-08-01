using Shared.Domain;

namespace ShippingModule.Domain.Shipments.DomainEvents;

public record ShipmentCanceled(
    Guid AggregateId,
    DateTime CanceledAt) : DomainEvent(AggregateId);