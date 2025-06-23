using Shared.Domain;
using ShipmentModule.Domain.Shipments.DomainEvents;
using ShipmentModule.Domain.Shipments.Enums;
using ShipmentModule.Domain.Shipments.Exceptions;

namespace ShipmentModule.Domain.Shipments.Aggregates;

public class Shipment : AggregateRoot<Guid>
{
    public Guid OrderId { get; private set; }
    public string Carrier { get; private set; }
    public string TrackingNumber { get; private set; }
    public ShipmentStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? DispatchedAt { get; private set; }
    public DateTime? DeliveredAt { get; private set; }
    public DateTime? CanceledAt { get; private set; }
    
    public int DeliveryAttempts { get; private set; } = 0;
    public const int MaxDeliveryAttempts = 3;

    private Shipment()
    {
    }

    private Shipment(Guid orderId, string carrier, string trackingNumber) : base(Guid.NewGuid())
    {
        OrderId = orderId;
        Carrier = carrier;
        TrackingNumber = trackingNumber;
        Status = ShipmentStatus.Created;
        CreatedAt = DateTime.UtcNow;

        AddDomainEvent(new ShipmentCreated(Id, OrderId, Carrier, TrackingNumber));
    }

    public static Shipment Create(Guid orderId, string carrier, string trackingNumber)
        => new(orderId, carrier, trackingNumber);

    public void Dispatch()
    {
        if (Status != ShipmentStatus.Created)
            throw new OnlyNewlyCreatedShipmentsCanBeDispatchedException();

        Status = ShipmentStatus.Dispatched;
        DispatchedAt = DateTime.UtcNow;

        AddDomainEvent(new ShipmentDispatched(Id, DispatchedAt.Value));
    }

    public void ConfirmDelivery()
    {
        if (Status != ShipmentStatus.Dispatched)
            throw new ShipmentMustBeDispatchedBeforeDeliveryException();

        Status = ShipmentStatus.Delivered;
        DeliveredAt = DateTime.UtcNow;

        AddDomainEvent(new ShipmentDelivered(Id, DeliveredAt.Value));
    }

    public void Cancel()
    {
        if (Status == ShipmentStatus.Delivered)
            throw new CannotCancelDeliveredShipmentException();

        Status = ShipmentStatus.Canceled;
        CanceledAt = DateTime.UtcNow;
        AddDomainEvent(new ShipmentCanceled(Id, CanceledAt.Value));
    }

    public void RetryDelivery()
    {
        if (Status != ShipmentStatus.Dispatched)
            throw new RetryOnlyAllowedForDispatchedShipmentsException();

        AddDomainEvent(new ShipmentDeliveryRetried(Id, DateTime.UtcNow));
    }
     
    public void MarkDeliveryFailed()
    {
        DeliveryAttempts++;
        if (DeliveryAttempts > MaxDeliveryAttempts)
            throw new BusinessRuleValidationException("Maximum delivery attempts exceeded");

        AddDomainEvent(new ShipmentDeliveryFailed(Id, DeliveryAttempts));
    }

}