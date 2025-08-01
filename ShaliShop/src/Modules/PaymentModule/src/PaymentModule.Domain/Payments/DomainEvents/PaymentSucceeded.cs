namespace PaymentModule.Domain.Payments.DomainEvents;

public record PaymentSucceeded(Guid AggregateId, Guid OrderId, string TransactionId) : DomainEvent(AggregateId);