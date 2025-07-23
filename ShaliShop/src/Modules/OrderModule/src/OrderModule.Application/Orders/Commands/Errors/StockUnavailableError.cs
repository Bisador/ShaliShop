namespace OrderModule.Application.Orders.Commands.Errors;

public record StockUnavailableError(string ProductName) : Error(ErrorCode, $"Insufficient stock for Product {ProductName}")
{
    public static string ErrorCode { get;  } = "STOCK_UNAVAILABLE";
}