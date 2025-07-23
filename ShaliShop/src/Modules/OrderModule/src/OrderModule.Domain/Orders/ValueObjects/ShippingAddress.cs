namespace OrderModule.Domain.Orders.ValueObjects;

public class ShippingAddress : ValueObject
{
    public string Street { get; } = null!;
    public string City { get; } = null!;
    public string ZipCode { get; } = null!;
    public string? State { get; }
    public string? Country { get; }

    private ShippingAddress()
    {
    }

    public ShippingAddress(string city, string street, string zipCode) : this()
    {
        Street = street;
        City = city;
        ZipCode = zipCode;
    }

    public ShippingAddress(string city, string street, string zipCode, string? state, string? country) : this()
    {
        Street = street;
        City = city;
        ZipCode = zipCode;
        State = state;
        Country = country;
    }

    public override string ToString() => $"{Street}, {City}, {State}, {ZipCode}, {Country}";

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return City;
        yield return Street;
        yield return ZipCode;
        if (!string.IsNullOrEmpty(State))
            yield return State;
        if (!string.IsNullOrEmpty(Country))
            yield return Country;
    }
}