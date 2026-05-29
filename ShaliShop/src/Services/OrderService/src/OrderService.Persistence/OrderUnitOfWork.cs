using Shared.Eventing;
using Shared.Persistence;

namespace OrderService.Persistence;

public class OrderUnitOfWork(OrderDbContext dbContext, DomainEventDispatcher dispatcher)
    : UnitOfWorkBase(dbContext, dispatcher);