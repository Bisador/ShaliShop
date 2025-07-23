using PaymentModule.Domain.Payments.Enums;

namespace PaymentModule.Domain.Payments.Rules;

public record OnlySucceededPaymentsCanBeRefunded(PaymentStatus Status) : IBusinessRule
{
    public bool IsBroken() => Status != PaymentStatus.Succeeded;

    public string Message => "Only succeeded payments can be refunded";
}