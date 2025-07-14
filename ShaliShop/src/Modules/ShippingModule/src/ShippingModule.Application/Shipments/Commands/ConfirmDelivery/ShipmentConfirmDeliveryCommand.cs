namespace ShippingModule.Application.Shipments.Commands.ConfirmDelivery;

public record ShipmentConfirmDeliveryCommand(Guid ShipmentId) : IRequest<Result>;