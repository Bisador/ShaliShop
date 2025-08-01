namespace CatalogModule.Domain.Products.DomainEvents;

public record ProductCreated(
    Guid AggregateId,
    string Name,
    string Category
) : DomainEvent(AggregateId);