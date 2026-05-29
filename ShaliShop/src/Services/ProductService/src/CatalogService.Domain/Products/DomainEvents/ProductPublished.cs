namespace CatalogService.Domain.Products.DomainEvents;

public record ProductPublished(
    Guid AggregateId
) : DomainEvent(AggregateId);