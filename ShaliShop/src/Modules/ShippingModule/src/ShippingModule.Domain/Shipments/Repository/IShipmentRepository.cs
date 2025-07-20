using ShippingModule.Domain.Shipments.Aggregates;

namespace ShippingModule.Domain.Shipments.Repository;

public interface IShipmentRepository
{
    Task<Shipment?> FindByOrderIdAsync(Guid commandOrderId, CancellationToken ct);
    Task<Shipment?> LoadAsync(Guid id, CancellationToken ct);
    Task SaveAsync(Shipment item, CancellationToken ct);
}