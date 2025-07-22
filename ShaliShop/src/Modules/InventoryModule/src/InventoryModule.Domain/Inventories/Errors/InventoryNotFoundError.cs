using Shared.Common;

namespace InventoryModule.Domain.Inventories.Errors;

public record InventoryNotFoundError(Guid Id) : DomainError($"Inventory item with ID {Id} was not found.");
 