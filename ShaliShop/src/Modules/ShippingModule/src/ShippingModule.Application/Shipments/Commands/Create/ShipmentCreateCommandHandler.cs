using ShippingModule.Application.Shipments.Commands.Errors; 

namespace ShippingModule.Application.Shipments.Commands.Create;

public class CreateShipmentCommandHandler(
    IShipmentRepository shipments,
    IShippingUnitOfWork unitOfWork
) : IRequestHandler<ShipmentCreateCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(ShipmentCreateCommand command, CancellationToken ct)
    {
        var existing = await shipments.FindByOrderIdAsync(command.OrderId, ct);
        if (existing is not null)
            return Result.Failure<Guid>(new ShipmentAlreadyExistsError(command.OrderId));

        var shipment = Shipment.Create(command.OrderId);

        await shipments.SaveAsync(shipment, ct);
        await unitOfWork.CommitAsync(ct);

        return shipment.Id;
    }
}