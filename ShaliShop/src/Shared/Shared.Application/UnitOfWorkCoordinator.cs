using System.Transactions;

namespace Shared.Application;

/// <summary>
/// Coordinates multiple unit-of-work commits into a single transactional scope.
/// Ensures atomicity across module boundaries.
/// </summary> 
public static class UnitOfWorkCoordinator
{
    public static async Task CommitAllAsync(
        params Func<Task>[] items)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        foreach (var item in items)
            await item();
        scope.Complete();
    }
}