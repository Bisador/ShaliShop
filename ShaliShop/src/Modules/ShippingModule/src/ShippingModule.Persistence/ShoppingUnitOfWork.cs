using Shared.Application.Events;
using Shared.Persistence;

namespace ShippingModule.Persistence;

public class ShippingUnitOfWork(ShipmentDbContext dbContext, DomainEventDispatcher dispatcher)
    : UnitOfWorkBase(dbContext, dispatcher);