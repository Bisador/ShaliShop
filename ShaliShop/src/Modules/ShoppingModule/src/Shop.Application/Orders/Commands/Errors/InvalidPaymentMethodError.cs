namespace Shop.Application.Orders.Commands.Errors;

public record InvalidPaymentMethodError() : Error(ErrorCode, "Unsupported payment method.")
{
    public static string ErrorCode { get; } = "INVALID_PAYMENT_METHOD";
}