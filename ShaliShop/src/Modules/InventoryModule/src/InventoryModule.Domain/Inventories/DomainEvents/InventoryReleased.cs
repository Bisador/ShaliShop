using Shared.Domain;

namespace InventoryModule.Domain.Inventories.DomainEvents;

public record InventoryReleased(Guid AggregateId, Guid ProductId, decimal QuantityReleased) : DomainEvent(AggregateId);