using Shared.Domain;

namespace ShippingService.Domain.Shipments.DomainEvents;

public record ShipmentCanceled(
    Guid AggregateId,
    DateTime CanceledAt) : DomainEvent(AggregateId);