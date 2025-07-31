using Shared.Domain;

namespace SharedModule.Domain.ValueObjects;

public class Money : ValueObject
{
    public decimal Amount { get; private set;}
    public string Currency { get; private set;} = Currencies.USD;

    private Money()
    {
    }

    public static Money From(decimal amount, string? currency = null) =>
        new(amount, currency);

    public static Money Empty(string? currency = null) =>
        new(0, currency);

    public Money(decimal amount, string? currency = null)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(amount);
        currency ??= Currencies.USD;
        Amount = amount;
        Currency = currency;
    }

    public override string ToString() => $"{Amount:0.00} {Currency}";

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }
}