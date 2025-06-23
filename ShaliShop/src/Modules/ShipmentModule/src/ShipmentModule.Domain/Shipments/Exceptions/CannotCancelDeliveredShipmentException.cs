using Shared.Domain;

namespace ShipmentModule.Domain.Shipments.Aggregates;

public class CannotCancelDeliveredShipmentException() : BusinessRuleValidationException("Cannot cancel a delivered shipment");