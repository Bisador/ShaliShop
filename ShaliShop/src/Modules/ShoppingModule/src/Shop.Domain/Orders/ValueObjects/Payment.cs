using Shop.Domain.Orders.Enums;

namespace Shop.Domain.Orders.ValueObjects;

public class Payment : ValueObject
{
    public string TransactionId { get; } = null!;
    public PaymentMethod PaymentMethod { get; }
    public DateTime PaidAt { get; }

    private Payment()
    {
    }

    public Payment(string transactionId, PaymentMethod paymentMethod, DateTime paidAt) : this()
    {
        TransactionId = transactionId;
        PaymentMethod = paymentMethod;
        PaidAt = paidAt;
    }
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return TransactionId;
        yield return PaymentMethod;
        yield return PaidAt;
    }
}