namespace Shop.Application.Orders.Commands.Return;

public record ReturnedItemNotFoundError() : Error(ErrorCode, "None of the returned items matched the order contents.")
{
    public static string ErrorCode { get; } = "RETURNED_ITEM_NOT_FOUND";
}