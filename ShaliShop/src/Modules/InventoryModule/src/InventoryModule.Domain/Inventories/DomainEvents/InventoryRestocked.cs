using Shared.Domain;

namespace InventoryModule.Domain.Inventories.DomainEvents;

public record InventoryRestocked(Guid AggregateId, Guid ProductId, decimal QuantityAdded) : DomainEvent(AggregateId);