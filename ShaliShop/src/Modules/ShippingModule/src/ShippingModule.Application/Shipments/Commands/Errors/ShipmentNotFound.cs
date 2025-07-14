namespace ShippingModule.Application.Shipments.Commands.Errors;

public record ShipmentNotFoundError(Guid ShipmentId) : Error(ErrorCode, $"Shipment {ShipmentId} not found.")
{
    public static string ErrorCode => "SHIPMENT_NOT_FOUND";
}