 
namespace ShippingService.Application.Shipments.Commands.Dispatch;

public record ShipmentDispatchCommand(
    Guid ShipmentId,
    string Carrier,
    string TrackingNumber
) : ICommand;