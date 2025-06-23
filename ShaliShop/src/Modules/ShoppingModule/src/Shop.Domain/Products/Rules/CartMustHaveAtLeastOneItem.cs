 

namespace Shop.Domain.Products.Rules;

public record StockCannotBeNegative(int Stock) : IBusinessRule
{
    public bool IsBroken() => Stock < 0;
    public string Message => "Stock can not be negative.";
}

 