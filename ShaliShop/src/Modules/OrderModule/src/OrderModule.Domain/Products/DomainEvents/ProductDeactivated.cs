namespace OrderModule.Domain.Products.DomainEvents;

public record ProductDeactivated(Guid ProductId) : DomainEvent;