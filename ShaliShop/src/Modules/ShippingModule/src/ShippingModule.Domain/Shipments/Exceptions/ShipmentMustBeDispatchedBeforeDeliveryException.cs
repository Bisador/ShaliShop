using Shared.Domain;

namespace ShippingModule.Domain.Shipments.Exceptions;

public class ShipmentMustBeDispatchedBeforeDeliveryException(bool isDispatched) : DomainException("Shipment must be dispatched before delivery")
{
    public override bool IsBroken() => !isDispatched;
}