using InventoryModule.Domain.Inventories.Aggregates;
using Shared.Common;

namespace InventoryModule.Domain.Inventories.Repository;

public interface IInventoryRepository
{
    //Task ReserveStockAsync(object variantSku, object quantity, CancellationToken ct);
    Task<Result> TryReserveStockAsync(Guid itemProductId, int itemQuantity, CancellationToken ct);
    Task<Inventory?> LoadAsync(Guid commandInventoryId, CancellationToken ct);
    Task SaveAsync(Inventory shipment, CancellationToken ct);
}