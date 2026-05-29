using Shared.Domain;

namespace InventoryService.Domain.Inventories.DomainEvents;

public record InventoryReserved(Guid AggregateId, Guid ProductId, decimal QuantityReserved) : DomainEvent(AggregateId);