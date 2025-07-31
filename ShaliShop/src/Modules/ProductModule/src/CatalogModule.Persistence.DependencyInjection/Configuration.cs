using Microsoft.Extensions.DependencyInjection;

namespace CatalogModule.Persistence.DependencyInjection;

public static class Configuration
{
    public static IServiceCollection AddCatalogModulePersistence(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<CatalogDbContext>(options =>
            options.UseSqlServer(connectionString,
                b => b.MigrationsAssembly(AssemblyReference.GetAssemblyReference.FullName)));

        return services;
    }
}