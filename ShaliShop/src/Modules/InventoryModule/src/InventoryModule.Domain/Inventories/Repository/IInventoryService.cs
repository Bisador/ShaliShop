using Shared.Common;

namespace InventoryModule.Domain.Inventories.Repository;

public interface IInventoryService
{
    //Task ReserveStockAsync(object variantSku, object quantity, CancellationToken ct);
    Task<Result> TryReserveStockAsync(Guid itemProductId, int itemQuantity, CancellationToken ct);
}