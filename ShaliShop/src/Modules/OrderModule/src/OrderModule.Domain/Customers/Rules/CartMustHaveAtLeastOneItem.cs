namespace OrderModule.Domain.Customers.Rules;

public class FullNameIsRequired(string fullName) : BusinessRuleValidationException("FullName is required.")
{
    public override bool IsBroken() => string.IsNullOrWhiteSpace(fullName);
}