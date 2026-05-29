using Shared.Eventing;
using Shared.Persistence;

namespace CheckoutService.Persistence;

public sealed class CheckoutUnitOfWork(CheckoutDbContext dbContext, DomainEventDispatcher dispatcher)
    : UnitOfWorkBase(dbContext, dispatcher);