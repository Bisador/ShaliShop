namespace Shop.Application.Orders.Commands.Errors;

public record StockUnavailableError( string ProductName) : Error("STOCK_UNAVAILABLE", $"Insufficient stock for Product {ProductName}");