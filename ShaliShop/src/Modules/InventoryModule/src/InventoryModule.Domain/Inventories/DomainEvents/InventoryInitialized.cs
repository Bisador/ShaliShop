using Shared.Domain;

namespace InventoryModule.Domain.Inventories.DomainEvents;

public record InventoryInitialized(Guid AggregateId, Guid ProductId, decimal InitialQuantity) : DomainEvent(AggregateId);