namespace ProductModule.Domain.Products.DomainEvents;

public record ProductVariantAdded(Guid Id, string Sku) : DomainEvent;