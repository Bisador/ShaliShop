using Shared.Application.Events;
using Shared.Persistence;

namespace OrderModule.Persistence;

public class OrderUnitOfWork(OrderDbContext dbContext, DomainEventDispatcher dispatcher)
    : UnitOfWorkBase(dbContext, dispatcher);