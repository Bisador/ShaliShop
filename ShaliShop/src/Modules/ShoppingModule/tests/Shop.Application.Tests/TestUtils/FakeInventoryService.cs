using Shop.Application.Orders.Commands.Errors;

namespace Shop.Application.Tests.TestUtils;

public class FakeInventoryService : IInventoryService
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
}