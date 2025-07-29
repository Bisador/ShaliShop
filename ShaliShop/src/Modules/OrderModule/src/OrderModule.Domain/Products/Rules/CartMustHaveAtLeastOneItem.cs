namespace OrderModule.Domain.Products.Rules;

public class StockCannotBeNegative(int stock) : BusinessRuleValidationException("Stock can not be negative.")
{
    public override bool IsBroken() => stock < 0;
}