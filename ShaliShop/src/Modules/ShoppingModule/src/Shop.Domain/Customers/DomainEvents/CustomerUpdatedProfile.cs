namespace Shop.Domain.Customers.DomainEvents;

public record CustomerUpdatedProfile(Guid CustomerId) : DomainEvent;