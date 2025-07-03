using InventoryModule.Domain.Inventories.Aggregates;
using InventoryModule.Domain.Inventories.DomainEvents; 

namespace InventoryModule.Domain.Test;

public class InventoryTests
{
    [Fact]
    public void Initializing_inventory_should_set_properties_and_raise_event()
    {
        var productId = Guid.NewGuid();
        var initialQuantity = 50;
        
        var inventory = Inventory.Initialize(productId, initialQuantity);

        inventory.ProductId.Should().Be(productId);
        inventory.QuantityOnHand.Should().Be(50);
        inventory.Reserved.Should().Be(0);

        inventory.Events.Any(e =>
            e is InventoryInitialized i &&
            i.InventoryId == inventory.Id &&
            i.ProductId == productId &&
            i.InitialQuantity == initialQuantity).Should().BeTrue();
    }
    
    [Fact]
    public void Reserving_inventory_within_available_should_increase_reserved_and_raise_event()
    {
        var productId = Guid.NewGuid();
        var inventory = Inventory.Initialize(productId, 20);

        inventory.Reserve(10);

        inventory.Reserved.Should().Be(10);
        inventory.QuantityOnHand.Should().Be(20); // still total on hand
        inventory.Events.Any(e =>
            e is InventoryReserved reserved &&
            reserved.InventoryId == inventory.Id &&
            reserved.ProductId == productId &&
            reserved.QuantityReserved == 10).Should().BeTrue();
    }

    [Fact]
    public void Reserving_more_than_available_should_throw()
    {
        var productId = Guid.NewGuid();
        var inventory = Inventory.Initialize(productId, 5); // Total on hand

        var act = () => inventory.Reserve(6); // Exceeds available

        act.Should().Throw<BusinessRuleValidationException>();
    }

    [Fact]
    public void Releasing_reserved_inventory_should_decrease_reserved_and_raise_event()
    {
        var productId = Guid.NewGuid();
        var inventory = Inventory.Initialize(productId, 10);

        inventory.Reserve(6);
        inventory.Release(4);

        inventory.Reserved.Should().Be(2); // originally 6, released 4
        inventory.Events.Any(e =>
            e is InventoryReleased released &&
            released.InventoryId == inventory.Id &&
            released.ProductId == productId &&
            released.QuantityReleased == 4).Should().BeTrue();
    }

    [Fact]
    public void Restocking_inventory_should_increase_quantity_and_raise_event()
    {
        var productId = Guid.NewGuid();
        var inventory = Inventory.Initialize(productId, 15);

        inventory.Restock(10); // Adding more units

        inventory.QuantityOnHand.Should().Be(25);
        inventory.Reserved.Should().Be(0); // unchanged

        inventory.Events.Any(e =>
            e is InventoryRestocked restocked &&
            restocked.InventoryId == inventory.Id &&
            restocked.ProductId == productId &&
            restocked.QuantityAdded == 10).Should().BeTrue();
    }
    
    [Fact]
    public void Reserving_below_low_stock_threshold_should_raise_LowStockDetected_once()
    {
        var productId = Guid.NewGuid();
        var inventory = Inventory.Initialize(productId, 10);
        inventory.SetLowStockThreshold(5); // alert if <5 available

        inventory.Reserve(4); // available = 6 → alert should NOT fire
        inventory.Events.Should().NotContain(e => e is LowStockDetected);

        inventory.Reserve(2); // available = 4 → alert SHOULD fire
        inventory.Events.Any(e =>
            e is LowStockDetected l &&
            l.InventoryId == inventory.Id &&
            l.Available == 4 &&
            l.Threshold == 5).Should().BeTrue();

        inventory.Reserve(1); // available = 3 → alert should NOT fire again
        inventory.Events.Count(e => e is LowStockDetected).Should().Be(1);
    }

    [Fact]
    public void Low_stock_alert_should_fire_again_after_restock_resets_it()
    {
        var productId = Guid.NewGuid();
        var inventory = Inventory.Initialize(productId, 10);
        inventory.SetLowStockThreshold(5);

        inventory.Reserve(6); // available = 4 → alert fires
        inventory.Events.Any(e => e is LowStockDetected).Should().BeTrue();

        inventory.Reserve(1); // available = 3 → alert should NOT fire again
        inventory.Events.Count(e => e is LowStockDetected).Should().Be(1);

        inventory.Restock(5); // available = 8 → resets alert flag
        inventory.Reserve(4); // available = 4 → alert fires AGAIN

        inventory.Events.Count(e => e is LowStockDetected).Should().Be(2);
    }
    
    [Fact]
    public void LowStockDetected_should_reset_after_restock_and_fire_again_on_threshold_cross()
    {
        var productId = Guid.NewGuid();
        var inventory = Inventory.Initialize(productId, 10);
        inventory.SetLowStockThreshold(5);

        // Cross threshold once
        inventory.Reserve(6); // Available = 4
        inventory.Events.Any(e => e is LowStockDetected).Should().BeTrue();

        // Fire suppression confirmed
        inventory.Reserve(1); // Available = 3
        inventory.Events.Count(e => e is LowStockDetected).Should().Be(1);

        // Reset by restock
        inventory.Restock(5); // Available = 8
        inventory.Events.Any(e => e is InventoryRestocked).Should().BeTrue();

        // Cross threshold again
        inventory.Reserve(4); // Available = 4
        inventory.Events.Count(e => e is LowStockDetected).Should().Be(2);
    }

}