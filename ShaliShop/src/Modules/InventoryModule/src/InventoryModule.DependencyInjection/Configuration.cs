using InventoryModule.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryModule.DependencyInjection;

public static class Configuration
{
    public static IServiceCollection AddInventoryModule(this IServiceCollection services, string connectionString)
    {
        //services.AddScoped<IInventoryReadRepository, InventoryReadRepository>();
        //services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<InventoryGetAllQuery>());
        
        services.AddDbContext<InventoryDbContext>(options =>
            options.UseSqlServer(connectionString,
                b => b.MigrationsAssembly(AssemblyReference.GetAssemblyReference.FullName)));

        return services;
    }
}