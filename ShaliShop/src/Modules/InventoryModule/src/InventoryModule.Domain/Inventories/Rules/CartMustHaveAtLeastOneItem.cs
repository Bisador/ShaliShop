using Shared.Domain;

namespace InventoryModule.Domain.Inventories.Rules;

public class NotEnoughAvailableInventoryToReserve(decimal quantity) : DomainException("Not enough available inventory to reserve.")
{
    public override bool IsBroken() => quantity <= 0;
}