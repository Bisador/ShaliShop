namespace PaymentService.Domain.Payments.DomainEvents;

public record RefundIssued(Guid AggregateId, Guid OrderId, string Reason) : DomainEvent(AggregateId);