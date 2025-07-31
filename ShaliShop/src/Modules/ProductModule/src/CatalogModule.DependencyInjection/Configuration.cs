using CatalogModule.Application.Abstraction.Products;
using CatalogModule.Application.Products.Queries.GetAll;
using CatalogModule.Persistence;
using CatalogModule.Persistence.Products;
using Microsoft.Extensions.DependencyInjection;

namespace CatalogModule.DependencyInjection;

public static class Configuration
{
    public static IServiceCollection AddCatalogModule(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<IProductReadRepository, ProductReadRepository>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<ProductGetAllQuery>());

        services.AddDbContext<CatalogDbContext>(options =>
            options.UseSqlServer(connectionString,
                b => b.MigrationsAssembly(AssemblyReference.GetAssemblyReference.FullName)));

        return services;
    }
}