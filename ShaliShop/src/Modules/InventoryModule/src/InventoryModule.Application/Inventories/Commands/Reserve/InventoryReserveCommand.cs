namespace InventoryModule.Application.Inventories.Commands.Reserve;

public record InventoryReserveCommand(
    Guid InventoryId,
    int Quantity
) : ICommand;