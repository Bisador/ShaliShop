using Shop.Domain.Payments.Enums;

namespace Shop.Domain.Payments.Rules;

public record OnlySucceededPaymentsCanBeRefunded(PaymentStatus Status) : IBusinessRule
{
    public bool IsBroken() => Status != PaymentStatus.Succeeded;

    public string Message => "Only succeeded payments can be refunded";
}