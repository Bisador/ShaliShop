using PaymentService.Domain.Payments.Enums;

namespace PaymentService.Domain.Payments.Rules;

public class CannotSucceedCancelledOrCompletedPayment(PaymentStatus status) : DomainException("Cannot succeed cancelled or completed payment.")
{
    public override bool IsBroken() => status != PaymentStatus.Pending;
}