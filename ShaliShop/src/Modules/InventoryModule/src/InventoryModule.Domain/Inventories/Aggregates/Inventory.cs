using InventoryModule.Domain.Inventories.DomainEvents;
using InventoryModule.Domain.Inventories.Exceptions;
using Shared.Domain;

namespace InventoryModule.Domain.Inventories.Aggregates;

public sealed class Inventory : AggregateRoot<Guid>
{
    public Guid ProductId { get; private set; }
    public decimal QuantityOnHand { get; private set; }
    public decimal Reserved { get; private set; }

    private decimal? _lowStockThreshold; 
    private bool _hasBeenDepleted;  
    private bool _lowStockAlertFired; 
 
    private Inventory()
    {
    }

    private Inventory(Guid productId, decimal initialQuantity)
    {
        ProductId = productId;
        QuantityOnHand = initialQuantity;
        Reserved = 0;

        AddDomainEvent(new InventoryInitialized(Id, productId, initialQuantity));
    }

    public static Inventory Initialize(Guid productId, decimal quantity)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(quantity);
        return new Inventory(productId, quantity);
    }
     
    public void Reserve(int quantity)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);

        if (quantity > AvailableToReserve())
            throw new NotEnoughAvailableInventoryToReserveException();
        
        Reserved += quantity;
        var available = AvailableToReserve();

        AddDomainEvent(new InventoryReserved(Id, ProductId, quantity));

        if (available == 0 && !_hasBeenDepleted)
        {
            AddDomainEvent(new InventoryDepleted(Id, ProductId));
            _hasBeenDepleted = true;
        }
        
        if (available < _lowStockThreshold &&
            !_lowStockAlertFired)
        {
            AddDomainEvent(new LowStockDetected(Id, ProductId, available, _lowStockThreshold.Value));
            _lowStockAlertFired = true;
        }
    }
      
    public void Release(int quantity)
    {
        if (quantity <= 0 || quantity > Reserved)
            throw new InvalidReleaseQuantityException();

        Reserved -= quantity;
        AddDomainEvent(new InventoryReleased(Id, ProductId, quantity));

        if (_lowStockThreshold.HasValue && AvailableToReserve() < _lowStockThreshold)
        {
            AddDomainEvent(new LowStockDetected(Id, ProductId, AvailableToReserve(), _lowStockThreshold.Value));
        }
    }

    public void Restock(int quantity)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);

        QuantityOnHand += quantity;
        _hasBeenDepleted = false;
        _lowStockAlertFired = false;

        AddDomainEvent(new InventoryRestocked(Id, ProductId, quantity));
    }
  
    public void SetLowStockThreshold(int quantity)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);

        _lowStockThreshold = quantity;
        _lowStockAlertFired = false;  
    }
    
    private decimal AvailableToReserve() => QuantityOnHand - Reserved;
}