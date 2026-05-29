 
namespace ShippingService.Application.Shipments.Commands.Create;

public record ShipmentCreateCommand(Guid OrderId) : ICommand<Guid>;
