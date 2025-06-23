using Shared.Domain;

namespace ShipmentModule.Domain.Shipments.Exceptions;

public class ShipmentMustBeDispatchedBeforeDeliveryException():BusinessRuleValidationException("Shipment must be dispatched before delivery");