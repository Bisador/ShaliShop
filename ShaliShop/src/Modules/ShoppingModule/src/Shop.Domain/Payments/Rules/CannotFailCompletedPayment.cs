using Shop.Domain.Payments.Enums;

namespace Shop.Domain.Payments.Rules;

public record CannotFailCompletedPayment(PaymentStatus Status) : IBusinessRule
{
    public bool IsBroken() => Status != PaymentStatus.Pending;

    public string Message => "Cannot fail completed payment";
}