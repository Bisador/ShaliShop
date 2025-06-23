using ShipmentModule.Domain.Shipments.Aggregates;

namespace ShipmentModule.Domain.Test;

public static class ShipmentFixture
{
    public static Shipment Create(
        string carrier = "UPS",
        string tracking = "XYZ-789",
        Guid? orderId = null)
    {
        return Shipment.Create(
            orderId ?? Guid.NewGuid(),
            carrier,
            tracking);
    }
}