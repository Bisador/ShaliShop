using Shared.Domain;

namespace ShippingModule.Domain.Shipments.Exceptions;

public class ShipmentMustBeDispatchedBeforeDeliveryException():BusinessRuleValidationException("Shipment must be dispatched before delivery");