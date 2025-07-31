using Microsoft.Extensions.DependencyInjection;

namespace CheckoutModule.Persistence.DependencyInjection;

public static class Configuration
{
    public static IServiceCollection AddCheckoutModulePersistence(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<CheckoutDbContext>(options =>
            options.UseSqlServer(connectionString, 
                b => b.MigrationsAssembly(AssemblyReference.GetAssemblyReference.FullName)));

        return services;
    }
}