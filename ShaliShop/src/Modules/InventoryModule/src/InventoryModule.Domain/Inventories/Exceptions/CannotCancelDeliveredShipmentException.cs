using Shared.Domain;

namespace InventoryModule.Domain.Inventories.Exceptions;

public class NotEnoughAvailableInventoryToReserveException() : DomainException("Not enough available inventory to reserve.");