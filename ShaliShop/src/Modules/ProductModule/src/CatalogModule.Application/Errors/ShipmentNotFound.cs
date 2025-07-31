namespace CatalogModule.Application.Errors;

public record ProductNotFoundError(Guid Id) : Error(ErrorCode, $"Product {Id} not found.")
{
    public static string ErrorCode => "PRODUCT_NOT_FOUND";
}