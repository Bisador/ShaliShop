 
using InventoryModule.Domain.Inventories.Aggregates;
using Shop.Application.Orders.Commands.Errors;

namespace Shop.Application.Tests.TestUtils;

public class FakeInventoryRepository : IInventoryRepository
{
    private readonly HashSet<Guid> _unavailableProducts = [];

    public void SetUnavailable(Guid productId)
        => _unavailableProducts.Add(productId);

    public Task<Result> TryReserveStockAsync(Guid productId, int quantity, CancellationToken ct)
    {
        var result = _unavailableProducts.Contains(productId) ? 
            Result.Failure(new StockUnavailableError(productId.ToString())) : Result.Success();
        return Task.FromResult(result);
    }

    public Task<Inventory?> LoadAsync(Guid id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task SaveAsync(Inventory item, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}