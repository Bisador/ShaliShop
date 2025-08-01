namespace PaymentModule.Domain.Payments.DomainEvents;

public record PaymentInitiated(Guid AggregateId, Guid OrderId, Money Amount) : DomainEvent(AggregateId);