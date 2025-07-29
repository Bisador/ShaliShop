namespace Shared.Domain;

public class BusinessRuleValidationException(string message) : Exception(message)
{
    public static string ErrorCode => "BUSINESS_RULE";

    public virtual bool IsBroken() => true;
}
  
 