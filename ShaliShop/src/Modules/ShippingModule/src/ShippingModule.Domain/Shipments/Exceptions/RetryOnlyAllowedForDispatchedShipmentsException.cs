using Shared.Domain;

namespace ShippingModule.Domain.Shipments.Exceptions;

public class RetryOnlyAllowedForDispatchedShipmentsException(bool isDispatched) : BusinessRuleValidationException("Retry only allowed for dispatched shipments")
{
    public override bool IsBroken()
    {
        return !isDispatched;
    }
}