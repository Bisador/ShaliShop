 
using Shared.Application.Events;
using Shared.Persistence;

namespace CheckoutModule.Persistence;

public sealed class CheckoutUnitOfWork(CheckoutDbContext dbContext, DomainEventDispatcher dispatcher)
    : UnitOfWorkBase(dbContext, dispatcher);