using Shared.Eventing;
using Shared.Persistence;

namespace ShippingService.Persistence;

public class ShippingUnitOfWork(ShipmentDbContext dbContext, DomainEventDispatcher dispatcher)
    : UnitOfWorkBase(dbContext, dispatcher);