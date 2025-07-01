namespace Shared.Common;

public record DomainError(string Message) : Error("DOMAIN_RULE_VIOLATION", Message)
{
    public static DomainError Make(string message) => new DomainError(message);
}