namespace PaymentService.Domain.Payments.DomainEvents;

public record PaymentFailed(Guid AggregateId, Guid OrderId, string Reason) : DomainEvent(AggregateId);