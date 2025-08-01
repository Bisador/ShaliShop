namespace CatalogModule.Domain.Products.DomainEvents;

public record ProductPublished(
    Guid AggregateId
) : DomainEvent(AggregateId);