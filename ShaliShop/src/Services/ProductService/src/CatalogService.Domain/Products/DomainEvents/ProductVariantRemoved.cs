namespace CatalogService.Domain.Products.DomainEvents;

public record ProductVariantRemoved(Guid AggregateId, string Sku) : DomainEvent(AggregateId);