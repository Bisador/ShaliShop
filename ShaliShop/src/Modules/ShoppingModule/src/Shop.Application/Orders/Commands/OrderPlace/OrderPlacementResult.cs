using SharedModule.Domain.ValueObjects;

namespace Shop.Application.Orders.Commands.OrderPlace;

public record OrderPlacementResult(Guid OrderId, Money Total);