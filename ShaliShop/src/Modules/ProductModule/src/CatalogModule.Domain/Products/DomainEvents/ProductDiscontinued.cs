namespace CatalogModule.Domain.Products.DomainEvents;

public record ProductDiscontinued(
    Guid AggregateId
) : DomainEvent(AggregateId);