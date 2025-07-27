namespace CheckoutModule.Application.Models;

public record ShippingAddressDto(string Street, string City, string ZipCode, string? State = null, string? Country = null)
{
    public string Street { get; } = Street ?? throw new ArgumentNullException(nameof(Street));
    public string City { get; } = City ?? throw new ArgumentNullException(nameof(City));
    public string ZipCode { get; } = ZipCode ?? throw new ArgumentNullException(nameof(ZipCode));
}