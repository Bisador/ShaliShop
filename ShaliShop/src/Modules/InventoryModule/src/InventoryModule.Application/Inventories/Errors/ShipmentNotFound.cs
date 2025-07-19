namespace InventoryModule.Application.Inventories.Errors;

public record InventoryNotFoundError(Guid Id) : Error(ErrorCode, $"Inventory {Id} not found.")
{
    public static string ErrorCode => "INVENTORY_NOT_FOUND";
}