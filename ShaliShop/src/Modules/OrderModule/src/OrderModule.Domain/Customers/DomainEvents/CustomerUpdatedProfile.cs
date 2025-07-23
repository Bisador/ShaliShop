namespace OrderModule.Domain.Customers.DomainEvents;

public record CustomerUpdatedProfile(Guid CustomerId) : DomainEvent;