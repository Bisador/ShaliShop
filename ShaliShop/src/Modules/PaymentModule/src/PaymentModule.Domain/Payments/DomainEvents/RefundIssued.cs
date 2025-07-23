namespace PaymentModule.Domain.Payments.DomainEvents;

public record RefundIssued(Guid PaymentId, Guid OrderId, string Reason) : DomainEvent;