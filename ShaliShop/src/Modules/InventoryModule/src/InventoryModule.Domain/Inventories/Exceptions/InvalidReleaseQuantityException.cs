using Shared.Domain;

namespace InventoryModule.Domain.Inventories.Exceptions;

public class InvalidReleaseQuantityException() : DomainException("Invalid release quantity.");