using Shared.Domain;

namespace ShippingService.Domain.Shipments.Exceptions;

public class ShipmentMustBeDispatchedBeforeDeliveryException(bool isDispatched) : DomainException("Shipment must be dispatched before delivery")
{
    public override bool IsBroken() => !isDispatched;
}