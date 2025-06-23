using Shared.Domain;

namespace InventoryModule.Domain.Inventories.DomainEvents;

public record InventoryRestocked(Guid InventoryId, Guid ProductId, int QuantityAdded) : DomainEvent;