using CatalogModule.Application.Abstraction;  
using Shared.Eventing;
using Shared.Persistence;

namespace CatalogModule.Persistence;

public sealed class CatalogUnitOfWork(CatalogDbContext dbContext, DomainEventDispatcher dispatcher)
    : UnitOfWorkBase(dbContext, dispatcher), ICatalogUnitOfWork;