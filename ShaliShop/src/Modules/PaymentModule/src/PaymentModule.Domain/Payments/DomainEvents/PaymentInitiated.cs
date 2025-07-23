namespace PaymentModule.Domain.Payments.DomainEvents;

public record PaymentInitiated(Guid PaymentId, Guid OrderId, Money Amount) : DomainEvent;