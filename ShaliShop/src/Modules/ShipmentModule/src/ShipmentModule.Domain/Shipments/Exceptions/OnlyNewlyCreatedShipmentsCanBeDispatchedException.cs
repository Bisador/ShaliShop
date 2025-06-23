using Shared.Domain;

namespace ShipmentModule.Domain.Shipments.Exceptions;

public class OnlyNewlyCreatedShipmentsCanBeDispatchedException():BusinessRuleValidationException("Only newly created shipments can be dispatched");