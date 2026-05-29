using ShippingService.Domain.Shipments.Aggregates;

namespace ShippingService.Domain.Shipments.Repository;

public interface IShipmentRepository
{
    Task<Shipment?> FindByOrderIdAsync(Guid commandOrderId, CancellationToken ct);
    Task<Shipment?> LoadAsync(Guid id, CancellationToken ct);
    Task SaveAsync(Shipment item, CancellationToken ct);
}