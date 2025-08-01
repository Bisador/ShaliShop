using Shared.Application.Events;
using Shared.Persistence;

namespace InventoryModule.Persistence;

public sealed class InventoryUnitOfWork(InventoryDbContext dbContext, DomainEventDispatcher dispatcher)
    : UnitOfWorkBase(dbContext, dispatcher);