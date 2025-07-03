namespace Shared.Domain;

public class BusinessRuleValidationException : Exception
{
    public static string ErrorCode { get; set; } = "Business_Rule";
    public BusinessRuleValidationException(string message) : base(message)
    {
        
    }
    
    public BusinessRuleValidationException(IBusinessRule rule) : base(rule.Message)
    {
        BrokenRule = rule;
    }

    public IBusinessRule? BrokenRule { get; }
}
 


public interface IBusinessRule
{
    bool IsBroken();
    string Message { get; }
}
 