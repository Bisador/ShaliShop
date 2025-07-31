using CatalogModule.Api.Products;

namespace CatalogModule.Api;

public static class CatalogApiEndpoints
{
    public static IEndpointRouteBuilder MapCatalogEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapProductEndpoints(); 
        return endpoints;
    }
}