using Shared.Application.Queries;

namespace CatalogModule.Persistence.Products;

public static class ProductReadRepositoryExtensions
{
    public static IQueryable<Product> ApplyOrder(this IQueryable<Product> products, IStandardQuery query)
    {
        products = query.SortBy switch
        {
            "Price" => query.Descending
                ? products.OrderByDescending(p => p.Price)
                : products.OrderBy(p => p.Price),
            _ => query.Descending
                ? products.OrderByDescending(p => p.Name)
                : products.OrderBy(p => p.Name)
        };
        return products;
    }

    public static IQueryable<Product> ApplyFilter(this IQueryable<Product> products, IStandardQuery query)
    {
        // Filtering
        if (!string.IsNullOrWhiteSpace(query.SearchTerm))
        {
            products = products.Where(p =>
                p.Name.Contains(query.SearchTerm) ||
                p.Description.Contains(query.SearchTerm));
        }
        return products;
    }
}