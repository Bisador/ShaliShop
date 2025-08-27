namespace InventoryModule.Application.Inventories.Commands.Release;

public record InventoryReleaseCommand(
    Guid InventoryId,
    int Quantity
) : IRequest<Result>;