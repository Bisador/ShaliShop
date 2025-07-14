namespace ShippingModule.Application.Shipments.Commands.Cancel;

public record ShipmentCancelCommand(Guid ShipmentId) : IRequest<Result>;