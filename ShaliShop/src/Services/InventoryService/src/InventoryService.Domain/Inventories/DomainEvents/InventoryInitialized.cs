using Shared.Domain;

namespace InventoryService.Domain.Inventories.DomainEvents;

public record InventoryInitialized(Guid AggregateId, Guid ProductId, decimal InitialQuantity) : DomainEvent(AggregateId);