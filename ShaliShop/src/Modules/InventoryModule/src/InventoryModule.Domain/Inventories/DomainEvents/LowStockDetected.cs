using Shared.Domain;

namespace InventoryModule.Domain.Inventories.DomainEvents;

public record LowStockDetected(
    Guid InventoryId,
    Guid ProductId,
    decimal Available,
    decimal Threshold
) : DomainEvent;