using System.Reflection;
using FluentValidation; 
using Microsoft.Extensions.DependencyInjection;
using Shared.Application.Behavior;
using Shop.Application;

namespace Shop.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection RegisterApplicationLayer(this IServiceCollection services)
    {
        List<Assembly> assemblies =[
            ShopApplicationAssemblyReference.Assembly
        ];
        
        foreach (var assembly in assemblies)
        {
            services.AddValidatorsFromAssembly(assembly);
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(assembly);
                config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
            });
        }

        return services;
    }
}