using Shared.Domain;

namespace ShippingModule.Domain.Shipments.Exceptions;

public class RetryOnlyAllowedForDispatchedShipmentsException() : BusinessRuleValidationException("Retry only allowed for dispatched shipments");