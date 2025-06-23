using Shared.Domain;

namespace InventoryModule.Domain.Inventories.DomainEvents;

public record InventoryInitialized(Guid InventoryId, Guid ProductId, decimal InitialQuantity) : DomainEvent;