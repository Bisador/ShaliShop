using InventoryModule.Application.Inventories.Errors; 

namespace InventoryModule.Application.Inventories.Commands.Release;

public class InventoryReleaseCommandHandler(
    IInventoryRepository inventories,
    IInventoryUnitOfWork unitOfWork
) : IRequestHandler<InventoryReleaseCommand, Result>
{
    public async Task<Result> Handle(InventoryReleaseCommand command, CancellationToken ct)
    {
        var inventory = await inventories.LoadAsync(command.InventoryId, ct);
        if (inventory is null)
            return Result.Failure(new InventoryNotFoundError(command.InventoryId)); 

        inventory.Release(command.Quantity);

        await inventories.SaveAsync(inventory, ct);
        await unitOfWork.CommitAsync(ct);

        return Result.Success();
    }
}