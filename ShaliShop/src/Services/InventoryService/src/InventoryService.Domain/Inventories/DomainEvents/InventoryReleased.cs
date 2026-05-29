using Shared.Domain;

namespace InventoryService.Domain.Inventories.DomainEvents;

public record InventoryReleased(Guid AggregateId, Guid ProductId, decimal QuantityReleased) : DomainEvent(AggregateId);