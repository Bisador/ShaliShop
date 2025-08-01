namespace OrderModule.Domain.Customers.Rules;

public class FullNameIsRequired(string fullName) : DomainException("FullName is required.")
{
    public override bool IsBroken() => string.IsNullOrWhiteSpace(fullName);
}