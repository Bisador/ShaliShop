namespace Shop.Domain.Orders.ValueObjects;

public class PaymentInfo : ValueObject
{
    public string TransactionId { get; }
    public PaymentMethod PaymentMethod { get; }
    public DateTime PaidAt { get; }

    private PaymentInfo()
    {
    }

    public PaymentInfo(string transactionId, PaymentMethod paymentMethod, DateTime paidAt) : this()
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

public enum PaymentMethod
{
    Cash,
    Check
}