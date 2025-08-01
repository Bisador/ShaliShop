using Shared.Domain;

namespace InventoryModule.Domain.Inventories.DomainEvents;

public record InventoryReserved(Guid AggregateId, Guid ProductId, decimal QuantityReserved) : DomainEvent(AggregateId);