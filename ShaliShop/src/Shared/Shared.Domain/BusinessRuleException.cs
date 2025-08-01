namespace Shared.Domain;

public class DomainException(string message) : Exception(message)
{
    public static string ErrorCode => "DOMAIN_RULE";

    public virtual bool IsBroken() => true;
}
  
 