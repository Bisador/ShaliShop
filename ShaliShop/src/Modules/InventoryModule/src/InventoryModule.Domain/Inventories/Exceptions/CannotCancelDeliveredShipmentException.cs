using Shared.Domain;

namespace InventoryModule.Domain.Inventories.Exceptions;

public class NotEnoughAvailableInventoryToReserveException() : BusinessRuleValidationException("Not enough available inventory to reserve.");