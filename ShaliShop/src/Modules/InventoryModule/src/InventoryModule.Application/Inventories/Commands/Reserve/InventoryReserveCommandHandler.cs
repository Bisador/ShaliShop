using InventoryModule.Application.Inventories.Errors;

namespace InventoryModule.Application.Inventories.Commands.Reserve;

public class InventoryReserveCommandHandler(
    IInventoryRepository inventories,
    IInventoryUnitOfWork unitOfWork
) : IRequestHandler<InventoryReserveCommand, Result>
{
    public async Task<Result> Handle(InventoryReserveCommand command, CancellationToken ct)
    {
        var inventory = await inventories.LoadAsync(command.InventoryId, ct);
        if (inventory is null)
            return Result.Failure(new InventoryNotFoundError(command.InventoryId));

        inventory.Reserve(command.Quantity);

        await inventories.SaveAsync(inventory, ct);
        await unitOfWork.CommitAsync(ct);

        return Result.Success();
    }
}