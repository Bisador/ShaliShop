namespace ShippingService.Application.Shipments.Commands.Cancel;

public record ShipmentCancelCommand(Guid ShipmentId) : ICommand;