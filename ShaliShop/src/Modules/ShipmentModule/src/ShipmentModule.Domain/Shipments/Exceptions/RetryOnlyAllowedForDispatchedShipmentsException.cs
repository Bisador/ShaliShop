using Shared.Domain;

namespace ShipmentModule.Domain.Shipments.Exceptions;

public class RetryOnlyAllowedForDispatchedShipmentsException() : BusinessRuleValidationException("Retry only allowed for dispatched shipments");