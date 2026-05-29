using Shared.Domain;

namespace InventoryService.Domain.Inventories.DomainEvents;

public record InventoryRestocked(Guid AggregateId, Guid ProductId, decimal QuantityAdded) : DomainEvent(AggregateId);