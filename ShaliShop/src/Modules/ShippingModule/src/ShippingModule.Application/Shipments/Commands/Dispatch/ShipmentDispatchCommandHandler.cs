namespace ShippingModule.Application.Shipments.Commands.Dispatch;

public class ShipmentDispatchCommandHandler(
    IShipmentRepository shipments,
    IShippingUnitOfWork unitOfWork
) : IRequestHandler<ShipmentDispatchCommand, Result>
{
    public async Task<Result> Handle(ShipmentDispatchCommand command, CancellationToken ct)
    {
        var shipment = await shipments.LoadAsync(command.ShipmentId, ct);
        if (shipment is null)
            return Result.Failure($"Shipment {command.ShipmentId} not found.");

        shipment.Dispatch(command.Carrier, command.TrackingNumber);

        await shipments.SaveAsync(shipment, ct);
        await unitOfWork.CommitAsync(ct);

        return Result.Success();
    }
}