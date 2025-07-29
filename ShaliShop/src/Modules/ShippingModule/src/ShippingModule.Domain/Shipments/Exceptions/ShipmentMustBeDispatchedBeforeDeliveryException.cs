using Shared.Domain;

namespace ShippingModule.Domain.Shipments.Exceptions;

public class ShipmentMustBeDispatchedBeforeDeliveryException(bool isDispatched) : BusinessRuleValidationException("Shipment must be dispatched before delivery")
{
    public override bool IsBroken() => !isDispatched;
}