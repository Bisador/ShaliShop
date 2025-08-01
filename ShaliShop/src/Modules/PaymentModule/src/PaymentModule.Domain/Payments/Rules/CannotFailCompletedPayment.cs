using PaymentModule.Domain.Payments.Enums;

namespace PaymentModule.Domain.Payments.Rules;

public class CannotFailCompletedPayment(PaymentStatus status) : DomainException("Cannot fail completed payment")
{
    public override bool IsBroken() => status != PaymentStatus.Pending;
}