using Shared.Domain;

namespace InventoryService.Domain.Inventories.Exceptions;

public class NotEnoughAvailableInventoryToReserveException() : DomainException("Not enough available inventory to reserve.");