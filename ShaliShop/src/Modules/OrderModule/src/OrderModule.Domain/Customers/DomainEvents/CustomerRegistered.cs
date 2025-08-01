namespace OrderModule.Domain.Customers.DomainEvents;

public record CustomerRegistered(Guid AggregateId, string FullName, string Email) : DomainEvent(AggregateId);