

namespace CheckoutModule.Application.Carts.Errors;

public record StockUnavailableError(string ProductName) : Error(ErrorCode, $"Insufficient stock for Product {ProductName}")
{
    public static string ErrorCode => "STOCK_UNAVAILABLE";
}