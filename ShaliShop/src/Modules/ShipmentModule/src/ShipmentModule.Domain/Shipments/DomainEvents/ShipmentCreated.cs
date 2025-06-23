using Shared.Domain;

namespace ShipmentModule.Domain.Shipments.DomainEvents;

public record ShipmentCreated(
    Guid ShipmentId,
    Guid OrderId,
    string Carrier,
    string TrackingNumber) : DomainEvent;