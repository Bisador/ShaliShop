using Shared.Domain;

namespace InventoryModule.Domain.Inventories.DomainEvents;

public record InventoryDepleted(
    Guid AggregateId,
    Guid ProductId
) : DomainEvent(AggregateId);