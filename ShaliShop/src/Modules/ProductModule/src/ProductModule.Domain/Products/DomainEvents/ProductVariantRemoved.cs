using Shared.Domain;

namespace ProductModule.Domain.Products.DomainEvents;

public record ProductVariantRemoved(Guid Id, string Sku) : DomainEvent;