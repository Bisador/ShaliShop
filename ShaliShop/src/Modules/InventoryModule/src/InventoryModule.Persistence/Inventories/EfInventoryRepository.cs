using InventoryModule.Domain.Inventories.Aggregates;
using InventoryModule.Domain.Inventories.Errors;
using InventoryModule.Domain.Inventories.Exceptions;
using InventoryModule.Domain.Inventories.Repository;
using Shared.Common;

namespace InventoryModule.Persistence.Inventories;

public class EfInventoryRepository(InventoryDbContext context) : IInventoryRepository
{
    public Task<Inventory?> LoadAsync(Guid id, CancellationToken ct) =>
        context.Inventories.FindAsync([id], ct).AsTask();

    public Task SaveAsync(Inventory inventory, CancellationToken ct)
    {
        context.Update(inventory);
        return Task.CompletedTask;
    }

    public async Task<Result> TryReleaseStockAsync(Guid productId, int quantity, CancellationToken ct)
    {
        var inventory = await LoadAsync(productId, ct);
        if (inventory is null)
            return Result.Failure(new InventoryNotFoundError(productId));

        try
        {
            inventory.Release(quantity);
            await SaveAsync(inventory, ct);
            return Result.Success();
        }
        catch (InvalidReleaseQuantityException exception)
        {
            return Result.Failure(exception.Message);
        }
    }

    public async Task<Result> TryReserveStockAsync(Guid productId, int quantity, CancellationToken ct)
    {
        var inventory = await LoadAsync(productId, ct);
        if (inventory is null) 
            return Result.Failure(new InventoryNotFoundError(productId)); 
        
        inventory.Reserve(quantity);
        await SaveAsync(inventory, ct);

        return Result.Success();
    }

    public async Task<Result> RestockAsync(Guid productId, int quantity, CancellationToken ct)
    {
        var inventory = await LoadAsync(productId, ct);
        if (inventory is null)
            return Result.Failure(new InventoryNotFoundError(productId));  

        try
        {
            inventory.Restock(quantity);
            await SaveAsync(inventory, ct);
            return Result.Success();
        }
        catch (ArgumentOutOfRangeException exception)
        {
            return Result.Failure(exception.Message); 
        }
    }
}