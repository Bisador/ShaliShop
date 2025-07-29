 

namespace CheckoutModule.Application.Carts.Errors;

public record CartEmptyError() : Error(ErrorCode, "Cart is empty.")
{
    public static string ErrorCode { get; } = "CART_EMPTY";
}