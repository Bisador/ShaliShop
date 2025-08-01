using CatalogModule.Application.Abstraction; 
using Shared.Application.Events;
using Shared.Persistence;

namespace CatalogModule.Persistence;

public sealed class CatalogUnitOfWork(CatalogDbContext dbContext, DomainEventDispatcher dispatcher)
    : UnitOfWorkBase(dbContext, dispatcher), ICatalogUnitOfWork;