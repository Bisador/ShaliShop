using Microsoft.Extensions.DependencyInjection;

namespace CheckoutModule.Persistence.DependencyInjection;

public static class CheckoutModuleDependencyInjectionExtension
{
    public static IServiceCollection AddCheckoutModulePersistence(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<CheckoutDbContext>(options =>
            options.UseSqlServer(connectionString, 
                b => b.MigrationsAssembly(CheckoutAssemblyReference.GetAssemblyReference.FullName)));

        return services;
    }
}