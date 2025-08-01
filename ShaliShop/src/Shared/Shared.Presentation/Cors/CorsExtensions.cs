using Microsoft.Extensions.DependencyInjection;

namespace Shared.Presentation.Cors;

public static class CorsExtensions
{
    public static IServiceCollection AddCorsServices(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
                policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
        });
    
    public static IApplicationBuilder UseCustomCors(this IApplicationBuilder app) => app.UseCors("AllowAll");
}