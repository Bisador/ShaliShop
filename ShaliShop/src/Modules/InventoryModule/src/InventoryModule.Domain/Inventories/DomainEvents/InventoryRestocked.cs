using Shared.Domain;

namespace InventoryModule.Domain.Inventories.DomainEvents;

public record InventoryRestocked(Guid InventoryId, Guid ProductId, decimal QuantityAdded) : DomainEvent;