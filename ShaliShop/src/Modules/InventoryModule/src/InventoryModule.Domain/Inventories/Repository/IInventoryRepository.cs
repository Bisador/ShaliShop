using InventoryModule.Domain.Inventories.Aggregates;
using Shared.Common;

namespace InventoryModule.Domain.Inventories.Repository;

public interface IInventoryRepository
{
    Task<Result> TryReserveStockAsync(Guid productId, int quantity, CancellationToken ct);
    Task<Result> TryReleaseStockAsync(Guid productId, int quantity, CancellationToken ct);
    Task<Result> RestockAsync(Guid productId, int quantity, CancellationToken ct);

    Task<Inventory?> LoadAsync(Guid id, CancellationToken ct);
    Task SaveAsync(Inventory item, CancellationToken ct);
}