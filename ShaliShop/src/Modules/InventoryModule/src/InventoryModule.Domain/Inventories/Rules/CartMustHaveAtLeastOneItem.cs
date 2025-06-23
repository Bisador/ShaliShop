 

using Shared.Domain;

namespace InventoryModule.Domain.Inventories.Rules;

public record NotEnoughAvailableInventoryToReserve(decimal Quantity) : IBusinessRule
{
    public bool IsBroken() => Quantity <= 0;
    public string Message => "Not enough available inventory to reserve.";
}
 