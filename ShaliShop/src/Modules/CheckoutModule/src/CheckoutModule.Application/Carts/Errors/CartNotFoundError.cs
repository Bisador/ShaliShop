 

namespace CheckoutModule.Application.Carts.Errors;

public record CartNotFoundError() : Error(ErrorCode, "Cart not found.")
{
    public static string ErrorCode { get; } = "CART_NOT_FOUND";
}