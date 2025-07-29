using PaymentModule.Domain.Payments.Enums;

namespace PaymentModule.Domain.Payments.Rules;

public class CannotSucceedCancelledOrCompletedPayment(PaymentStatus status) : BusinessRuleValidationException("Cannot succeed cancelled or completed payment.")
{
    public override bool IsBroken() => status != PaymentStatus.Pending;
}