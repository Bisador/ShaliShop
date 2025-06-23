using Shared.Domain;

namespace InventoryModule.Domain.Inventories.DomainEvents;

public record InventoryReserved(Guid InventoryId, Guid ProductId, decimal QuantityReserved) : DomainEvent;