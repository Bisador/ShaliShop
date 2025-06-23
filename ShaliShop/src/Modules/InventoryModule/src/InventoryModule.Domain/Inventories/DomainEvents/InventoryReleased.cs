using Shared.Domain;

namespace InventoryModule.Domain.Inventories.DomainEvents;

public record InventoryReleased(Guid InventoryId, Guid ProductId, decimal QuantityReleased) : DomainEvent;