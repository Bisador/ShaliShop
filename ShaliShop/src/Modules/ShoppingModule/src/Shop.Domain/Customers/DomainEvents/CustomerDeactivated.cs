namespace Shop.Domain.Customers.DomainEvents;

public record CustomerDeactivated(Guid CustomerId) : DomainEvent;