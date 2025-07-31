using Microsoft.Extensions.DependencyInjection;

namespace InventoryModule.Persistence.DependencyInjection;

public static class Configuration
{
    public static IServiceCollection AddInventoryModulePersistence(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<InventoryDbContext>(options =>
            options.UseSqlServer(connectionString,
                b => b.MigrationsAssembly(AssemblyReference.GetAssemblyReference.FullName)));

        return services;
    }
}