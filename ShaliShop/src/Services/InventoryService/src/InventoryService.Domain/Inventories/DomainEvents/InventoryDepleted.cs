using Shared.Domain;

namespace InventoryService.Domain.Inventories.DomainEvents;

public record InventoryDepleted(
    Guid AggregateId,
    Guid ProductId
) : DomainEvent(AggregateId);