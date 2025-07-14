using Shared.Domain;

namespace ShippingModule.Domain.Shipments.DomainEvents;

 
public record ShipmentDispatched(
    Guid ShipmentId,
    Guid OrderId,
    DateTime DispatchedAt,
    string Carrier,
    string TrackingNumber
) : DomainEvent;
