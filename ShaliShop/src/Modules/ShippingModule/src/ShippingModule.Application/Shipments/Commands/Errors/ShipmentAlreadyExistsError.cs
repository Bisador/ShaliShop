namespace ShippingModule.Application.Shipments.Commands.Errors;

public record ShipmentAlreadyExistsError(Guid OrderId) : Error(ErrorCode, $"Shipment already exists for order {OrderId}.")
{
    public static string ErrorCode => "SHIPMENT_ALREADY_EXISTS";
}