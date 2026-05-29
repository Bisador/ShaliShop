using Shared.Domain;

namespace ShippingService.Domain.Shipments.Exceptions;

public class OnlyNewlyCreatedShipmentsCanBeDispatchedException():DomainException("Only newly created shipments can be dispatched");