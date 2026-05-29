namespace OrderService.Domain.Customers.DomainEvents;

public record CustomerUpdatedProfile(Guid AggregateId): DomainEvent(AggregateId);