using Shared.Domain;

namespace ShipmentModule.Domain.Shipments.Aggregates;

public class RetryOnlyAllowedForDispatchedShipmentsException() : BusinessRuleValidationException("Retry only allowed for dispatched shipments");