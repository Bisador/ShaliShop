using SharedModule.Domain.ValueObjects;

namespace OrderModule.Application.Orders.Commands.OrderPlace;

public record OrderPlacementResult(Guid OrderId, Money Total);