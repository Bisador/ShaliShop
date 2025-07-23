namespace OrderModule.Domain.Customers.DomainEvents;

public record CustomerRegistered(Guid CustomerId, string FullName, string Email) : DomainEvent;