using CatalogModule.Application;
using CatalogModule.Application.Abstraction;
using CatalogModule.Application.Abstraction.Products;
using CatalogModule.Domain.Products.Repository;
using CatalogModule.Persistence;
using CatalogModule.Persistence.Products;
using Microsoft.Extensions.DependencyInjection;

namespace CatalogModule.DependencyInjection;

public static class Configuration
{
    public static IServiceCollection AddCatalogModule(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<ICatalogUnitOfWork, CatalogUnitOfWork>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductReadRepository, ProductReadRepository>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(CatalogModuleApplicationAssemblyReference.Get()));

        services.AddDbContext<CatalogDbContext>(options =>
            options.UseSqlServer(connectionString,
                b => b.MigrationsAssembly(CatalogModulePersistenceAssemblyReference.GetAssemblyReference.FullName)));

        return services;
    }
}