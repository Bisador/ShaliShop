using Shared.Domain;

namespace ShippingModule.Domain.Shipments.Exceptions;

public class OnlyNewlyCreatedShipmentsCanBeDispatchedException():BusinessRuleValidationException("Only newly created shipments can be dispatched");