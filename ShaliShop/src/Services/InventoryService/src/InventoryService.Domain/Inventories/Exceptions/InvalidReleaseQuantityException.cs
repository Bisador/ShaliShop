using Shared.Domain;

namespace InventoryService.Domain.Inventories.Exceptions;

public class InvalidReleaseQuantityException() : DomainException("Invalid release quantity.");