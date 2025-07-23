using PaymentModule.Domain.Payments.Enums;

namespace PaymentModule.Domain.Payments.Rules;

public record CannotSucceedCancelledOrCompletedPayment(PaymentStatus Status) : IBusinessRule
{
    public bool IsBroken() => Status != PaymentStatus.Pending;

    public string Message => "Cannot succeed cancelled or completed payment";
}