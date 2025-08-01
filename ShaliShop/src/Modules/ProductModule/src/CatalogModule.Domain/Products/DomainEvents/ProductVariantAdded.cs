namespace CatalogModule.Domain.Products.DomainEvents;

public record ProductVariantAdded(Guid AggregateId, string Sku) : DomainEvent(AggregateId);