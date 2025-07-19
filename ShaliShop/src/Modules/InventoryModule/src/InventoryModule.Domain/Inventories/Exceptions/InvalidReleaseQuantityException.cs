using Shared.Domain;

namespace InventoryModule.Domain.Inventories.Exceptions;

public class InvalidReleaseQuantityException() : BusinessRuleValidationException("Invalid release quantity.");