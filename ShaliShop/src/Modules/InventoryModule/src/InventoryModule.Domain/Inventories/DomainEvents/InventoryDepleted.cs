using Shared.Domain;

namespace InventoryModule.Domain.Inventories.DomainEvents;

public record InventoryDepleted(
    Guid InventoryId,
    Guid ProductId
) : DomainEvent;