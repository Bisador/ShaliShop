namespace ShippingModule.Application.Shipments.Commands.Cancel;

public record ShipmentCancelCommand(Guid ShipmentId) : ICommand;