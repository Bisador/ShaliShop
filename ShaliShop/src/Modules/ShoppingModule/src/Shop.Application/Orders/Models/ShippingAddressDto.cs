namespace Shop.Application.Orders.Models;

public record ShippingAddressDto
{
    public required string Street { get; init; }
    public required string City { get; init; }
    public required string ZipCode { get; init; }
    public string? State { get; init; }
    public string? Country { get; init; }
}