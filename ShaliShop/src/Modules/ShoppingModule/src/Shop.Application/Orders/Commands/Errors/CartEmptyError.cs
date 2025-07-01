namespace Shop.Application.Orders.Commands.Errors;

public record CartEmptyError() : Error("CART_EMPTY", "Cannot place order with an empty cart.");