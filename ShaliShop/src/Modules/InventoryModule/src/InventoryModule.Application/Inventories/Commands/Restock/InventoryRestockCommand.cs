namespace InventoryModule.Application.Inventories.Commands.Restock;

public record InventoryRestockCommand(
    Guid InventoryId,
    int Quantity
) : IRequest<Result>;