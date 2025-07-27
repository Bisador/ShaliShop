namespace ProductModule.Domain.Products.DomainEvents;

public record ProductDiscontinued(
    Guid ProductId
) : DomainEvent;