using Shared.Domain;

namespace ShipmentModule.Domain.Shipments.Exceptions;

public class CannotCancelDeliveredShipmentException() : BusinessRuleValidationException("Cannot cancel a delivered shipment");