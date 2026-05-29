using Shared.Eventing;
using Shared.Persistence;

namespace InventoryService.Persistence;

public sealed class InventoryUnitOfWork(InventoryDbContext dbContext, DomainEventDispatcher dispatcher)
    : UnitOfWorkBase(dbContext, dispatcher);