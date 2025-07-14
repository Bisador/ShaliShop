using Shared.Domain;
using ShippingModule.Domain.Shipments.DomainEvents;
using ShippingModule.Domain.Shipments.Enums;
using ShippingModule.Domain.Shipments.Exceptions;

namespace ShippingModule.Domain.Shipments.Aggregates;

public class Shipment : AggregateRoot<Guid>
{
    public Guid OrderId { get; private set; }
    public ShipmentStatus Status { get; private set; }
    public string? Carrier { get; private set; }
    public string? TrackingNumber { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? DispatchedAt { get; private set; }
    public DateTime? DeliveredAt { get; private set; }
    public DateTime? CanceledAt { get; private set; }

    public int DeliveryAttempts { get; private set; }
    public const int MaxDeliveryAttempts = 3;

    private Shipment()
    {
    }

    private Shipment(Guid orderId) : base(Guid.NewGuid())
    {
        OrderId = orderId;
        Status = ShipmentStatus.Created;
        CreatedAt = DateTime.UtcNow;

        AddDomainEvent(new ShipmentCreated(Id, OrderId));
    }

    public static Shipment Create(Guid orderId)
        => new(orderId);

    public void Dispatch(string carrier, string trackingNumber)
    {
        if (Status != ShipmentStatus.Created)
            throw new OnlyNewlyCreatedShipmentsCanBeDispatchedException();

        Carrier = carrier;
        TrackingNumber = trackingNumber;
        Status = ShipmentStatus.Dispatched;
        DispatchedAt = DateTime.UtcNow;

        AddDomainEvent(new ShipmentDispatched(Id, OrderId, DispatchedAt.Value, carrier, trackingNumber));
    }

    public void ConfirmDelivery()
    {
        if (!IsDispatched)
            throw new ShipmentMustBeDispatchedBeforeDeliveryException();

        Status = ShipmentStatus.Delivered;
        DeliveredAt = DateTime.UtcNow;
        DeliveryAttempts = 0;

        AddDomainEvent(new ShipmentDelivered(Id, DeliveredAt.Value));
    }

    public void Cancel()
    {
        if (IsDelivered)
            throw new CannotCancelDeliveredShipmentException();

        Status = ShipmentStatus.Canceled;
        CanceledAt = DateTime.UtcNow;
        AddDomainEvent(new ShipmentCanceled(Id, CanceledAt.Value));
    }

    public void RetryDelivery()
    {
        if (!IsDispatched)
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

    private bool IsDispatched => Status == ShipmentStatus.Dispatched;
    private bool IsDelivered => Status == ShipmentStatus.Delivered;
    private bool IsCancellable => !IsDelivered;
}