namespace OrderModule.Application.Orders.Commands.Errors;

public record OrderNotFoundError(Guid OrderId) : Error(ErrorCode, $"Order {OrderId} not found")
{
    public static string ErrorCode { get; } = "ORDER_NOT_FOUND";
}