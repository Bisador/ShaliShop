namespace PaymentModule.Domain.Payments.DomainEvents;

public record PaymentFailed(Guid AggregateId, Guid OrderId, string Reason) : DomainEvent(AggregateId);