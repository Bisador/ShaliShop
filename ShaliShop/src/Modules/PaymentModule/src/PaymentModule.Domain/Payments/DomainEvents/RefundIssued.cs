namespace PaymentModule.Domain.Payments.DomainEvents;

public record RefundIssued(Guid AggregateId, Guid OrderId, string Reason) : DomainEvent(AggregateId);