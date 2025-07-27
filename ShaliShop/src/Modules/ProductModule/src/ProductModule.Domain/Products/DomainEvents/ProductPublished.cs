namespace ProductModule.Domain.Products.DomainEvents;

public record ProductPublished(
    Guid ProductId
) : DomainEvent;