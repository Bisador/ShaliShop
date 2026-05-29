namespace InventoryService.Application.Inventories.Commands.SetLowStockThreshold;

public record InventorySetLowStockThresholdCommand(
    Guid InventoryId,
    int Threshold
) : IRequest<Result>;