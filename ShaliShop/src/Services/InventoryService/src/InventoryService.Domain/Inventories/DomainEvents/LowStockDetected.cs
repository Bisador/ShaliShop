using Shared.Domain;

namespace InventoryService.Domain.Inventories.DomainEvents;

public record LowStockDetected(
    Guid AggregateId,
    Guid ProductId,
    decimal Available,
    decimal Threshold
) : DomainEvent(AggregateId);