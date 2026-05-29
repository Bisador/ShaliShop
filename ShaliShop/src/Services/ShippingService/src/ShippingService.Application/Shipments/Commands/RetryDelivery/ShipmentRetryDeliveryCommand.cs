namespace ShippingService.Application.Shipments.Commands.RetryDelivery;

public record ShipmentRetryDeliveryCommand(Guid ShipmentId) : IRequest<Result>;