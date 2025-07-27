namespace ProductModule.Domain.Products.DomainEvents;

public record ProductCreated(
    Guid ProductId,
    string Name,
    string Category
) : DomainEvent;