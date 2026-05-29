namespace ShippingService.Application.Shipments.Commands.ConfirmDelivery;

public record ShipmentConfirmDeliveryCommand(Guid ShipmentId) : IRequest<Result>;