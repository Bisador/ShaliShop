using PaymentModule.Domain.Payments.Enums;

namespace PaymentModule.Domain.Payments.Rules;

public record CannotFailCompletedPayment(PaymentStatus Status) : IBusinessRule
{
    public bool IsBroken() => Status != PaymentStatus.Pending;

    public string Message => "Cannot fail completed payment";
}