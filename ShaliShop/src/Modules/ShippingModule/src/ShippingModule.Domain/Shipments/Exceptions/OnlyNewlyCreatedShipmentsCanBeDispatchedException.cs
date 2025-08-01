using Shared.Domain;

namespace ShippingModule.Domain.Shipments.Exceptions;

public class OnlyNewlyCreatedShipmentsCanBeDispatchedException():DomainException("Only newly created shipments can be dispatched");