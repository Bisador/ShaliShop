using ShippingModule.Domain.Shipments.Aggregates;

namespace ShippingModule.Domain.Shipments.Repository;

public interface IShipmentRepository
{
    Task<Shipment?> FindByOrderIdAsync(Guid commandOrderId, CancellationToken ct);
    Task SaveAsync(Shipment shipment, CancellationToken ct);
    Task<Shipment?> LoadAsync(Guid shipmentId, CancellationToken ct);
}