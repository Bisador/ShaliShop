namespace Shop.Domain.Payments.DomainEvents;

public record PaymentFailed(Guid PaymentId, Guid OrderId, string Reason) : DomainEvent;