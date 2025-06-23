 

namespace Shop.Domain.Customers.Rules;

public record FullNameIsRequired(string FullName) : IBusinessRule
{
    public bool IsBroken() => string.IsNullOrWhiteSpace(FullName);
    public string Message => "FullName is required.";
}

 