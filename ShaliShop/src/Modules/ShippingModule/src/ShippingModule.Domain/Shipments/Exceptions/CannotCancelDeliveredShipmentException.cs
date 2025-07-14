using Shared.Domain;

namespace ShippingModule.Domain.Shipments.Exceptions;

public class CannotCancelDeliveredShipmentException() : BusinessRuleValidationException("Cannot cancel a delivered shipment");