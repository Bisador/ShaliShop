using Shared.Application.Queries;

namespace CatalogModule.Persistence.Products;

public static class ProductReadRepositoryExtensions
{
    public static IQueryable<Product> ApplyOrder(this IQueryable<Product> query, IStandardQuery parameter)
    {
        query = parameter.SortBy switch
        {
            "Price" => parameter.Descending
                ? query.OrderByDescending(p => p.Price)
                : query.OrderBy(p => p.Price),
            _ => parameter.Descending
                ? query.OrderByDescending(p => p.Name)
                : query.OrderBy(p => p.Name)
        };
        return query;
    }

    public static IQueryable<Product> ApplyFilter(this IQueryable<Product> query, IStandardQuery parameter)
    {
        // Filtering
        if (!string.IsNullOrWhiteSpace(parameter.SearchTerm))
        {
            query = query.Where(p =>
                p.Name.Contains(parameter.SearchTerm) ||
                p.Description.Contains(parameter.SearchTerm));
        }
        return query;
    }
}