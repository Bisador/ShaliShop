using Shared.Domain;

namespace InventoryModule.Domain.Inventories.Rules;

public class NotEnoughAvailableInventoryToReserve(decimal quantity) : BusinessRuleValidationException("Not enough available inventory to reserve.")
{
    public override bool IsBroken() => quantity <= 0;
}