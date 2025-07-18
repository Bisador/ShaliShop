using ShippingModule.Application.Shipments.Commands.Errors;

namespace ShippingModule.Application.Shipments.Commands.RetryDelivery;

public class ShipmentRetryDeliveryCommandHandler(
    IShipmentRepository shipments,
    IShippingUnitOfWork unitOfWork
) : IRequestHandler<ShipmentRetryDeliveryCommand, Result>
{
    public async Task<Result> Handle(ShipmentRetryDeliveryCommand command, CancellationToken ct)
    {
        var shipment = await shipments.LoadAsync(command.ShipmentId, ct);
        if (shipment is null)
            return Result.Failure(new ShipmentNotFoundError(command.ShipmentId)); 

        shipment.RetryDelivery();

        await shipments.SaveAsync(shipment, ct);
        await unitOfWork.CommitAsync(ct);

        return Result.Success();
    }
}