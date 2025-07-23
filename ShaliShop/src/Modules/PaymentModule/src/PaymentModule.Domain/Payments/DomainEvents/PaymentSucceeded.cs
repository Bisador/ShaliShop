namespace PaymentModule.Domain.Payments.DomainEvents;

public record PaymentSucceeded(Guid PaymentId, Guid OrderId, string TransactionId) : DomainEvent;