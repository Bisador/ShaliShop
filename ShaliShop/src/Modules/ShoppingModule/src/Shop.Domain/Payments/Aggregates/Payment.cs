using Shop.Domain.Payments.DomainEvents;
using Shop.Domain.Payments.Enums;
using Shop.Domain.Payments.Rules;

namespace Shop.Domain.Payments.Aggregates;

public sealed class Payment : AggregateRoot<Guid>
{
    public Guid OrderId { get; private set; }
    public Guid CustomerId { get; private set; }
    public Money Amount { get; private set; }
    public PaymentStatus Status { get; private set; }
    public string? TransactionId { get; private set; }
    public DateTime InitiatedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }

    private Payment()
    {
    }

    private Payment(Guid orderId, Guid customerId, Money amount) : base(Guid.NewGuid())
    {
        OrderId = orderId;
        CustomerId = customerId;
        Amount = amount;
        Status = PaymentStatus.Pending;
        InitiatedAt = DateTime.UtcNow;

        AddDomainEvent(new PaymentInitiated(Id, OrderId, Amount));
    }

    public static Payment Initiate(Guid orderId, Guid customerId, Money amount)
        => new(orderId, customerId, amount);

    public void Succeed(string transactionId)
    {
        CheckRule(new CannotSucceedCancelledOrCompletedPayment(Status));

        Status = PaymentStatus.Succeeded;
        TransactionId = transactionId;
        CompletedAt = DateTime.UtcNow;

        AddDomainEvent(new PaymentSucceeded(Id, OrderId, transactionId));
    }

    public void Fail(string reason)
    {
        CheckRule(new CannotFailCompletedPayment(Status));

        Status = PaymentStatus.Failed;
        CompletedAt = DateTime.UtcNow;

        AddDomainEvent(new PaymentFailed(Id, OrderId, reason));
    }

    public void IssueRefund(string reason)
    {
        CheckRule(new OnlySucceededPaymentsCanBeRefunded(Status));

        Status = PaymentStatus.Refunded;

        AddDomainEvent(new RefundIssued(Id, OrderId, reason));
    }
}