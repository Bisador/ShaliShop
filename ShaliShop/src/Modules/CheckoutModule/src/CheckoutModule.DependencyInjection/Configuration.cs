using CheckoutModule.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace CheckoutModule.DependencyInjection;

public static class Configuration
{
    public static IServiceCollection AddCheckoutModule(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<CheckoutDbContext>(options =>
            options.UseSqlServer(connectionString, 
                b => b.MigrationsAssembly(AssemblyReference.GetAssemblyReference.FullName)));

        return services;
    }
}