using ShippingModule.Domain.Shipments.Aggregates;

namespace ShippingModule.Domain.Test;

public static class ShipmentFixture
{
    public static Shipment Create(Guid? orderId = null)
    {
        return Shipment.Create(orderId ?? Guid.NewGuid());
    }
    
    public static Shipment CreateAndDispatch(
        Guid? orderId = null,
        string carrier = "UPS",
        string tracking = "XYZ-789")
    {
        var shipment=Shipment.Create(orderId ?? Guid.NewGuid());
        shipment.Dispatch(carrier, tracking);
        return shipment;
    }
}