namespace OrderModule.Application.Orders.Commands.Errors;

public record CartEmptyError() : Error(ErrorCode, "Cannot place order with an empty cart.")
{
    public static string ErrorCode { get; } = "CART_EMPTY";
}