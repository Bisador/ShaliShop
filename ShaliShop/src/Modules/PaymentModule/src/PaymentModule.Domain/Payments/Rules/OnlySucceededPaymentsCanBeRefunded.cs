using PaymentModule.Domain.Payments.Enums;

namespace PaymentModule.Domain.Payments.Rules;

public class OnlySucceededPaymentsCanBeRefunded(PaymentStatus status) : DomainException("Only succeeded payments can be refunded")
{
    public override bool IsBroken() => status != PaymentStatus.Succeeded;
}