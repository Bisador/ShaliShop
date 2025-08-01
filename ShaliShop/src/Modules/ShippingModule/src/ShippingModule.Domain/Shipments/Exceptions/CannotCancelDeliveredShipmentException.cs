using Shared.Domain;

namespace ShippingModule.Domain.Shipments.Exceptions;

public class CannotCancelDeliveredShipmentException(bool isDelivered) : DomainException("Cannot cancel a delivered shipment")
{
    public override bool IsBroken()
    {
        return isDelivered;
    }
}