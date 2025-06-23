namespace Shared.Domain;

public abstract class ValueObject: IEquatable<ValueObject>
{  
    protected abstract IEnumerable<object?> GetEqualityComponents();

    public bool Equals(ValueObject? other) => 
        other is not null && GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());

    public override bool Equals(object? obj)
    {
        if (obj is not ValueObject other) return false;
        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public override int GetHashCode() =>
        GetEqualityComponents()
            .Aggregate(1, HashCode.Combine);
    
    protected static void CheckRule(IBusinessRule rule)
    {
        if (rule.IsBroken())
            throw new BusinessRuleValidationException(rule);
    }
}