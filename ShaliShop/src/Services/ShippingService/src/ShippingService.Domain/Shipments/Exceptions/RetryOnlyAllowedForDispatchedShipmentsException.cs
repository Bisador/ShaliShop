using Shared.Domain;

namespace ShippingService.Domain.Shipments.Exceptions;

public class RetryOnlyAllowedForDispatchedShipmentsException(bool isDispatched) : DomainException("Retry only allowed for dispatched shipments")
{
    public override bool IsBroken()
    {
        return !isDispatched;
    }
}