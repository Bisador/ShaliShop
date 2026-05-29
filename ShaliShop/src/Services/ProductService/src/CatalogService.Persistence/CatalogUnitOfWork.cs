 
using Shared.Eventing;
using Shared.Persistence;

namespace CatalogService.Persistence;

public sealed class CatalogUnitOfWork(CatalogDbContext dbContext, DomainEventDispatcher dispatcher)
    : UnitOfWorkBase(dbContext, dispatcher), ICatalogUnitOfWork;