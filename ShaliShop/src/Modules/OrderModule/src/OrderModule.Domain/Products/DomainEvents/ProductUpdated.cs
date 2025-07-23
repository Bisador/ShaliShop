namespace OrderModule.Domain.Products.DomainEvents;

public record ProductUpdated(Guid ProductId) : DomainEvent;