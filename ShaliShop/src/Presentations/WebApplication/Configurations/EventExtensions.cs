using Shared.Eventing;
using Shared.Eventing.Abstraction;
using Shared.Messaging;  
using Shared.Telemetry;

namespace WebApplication.Configurations;

public static class EventExtensions
{
    public static IServiceCollection RegisterEventHandling(this IServiceCollection services, IWebHostEnvironment environment, string connectionString)
    {
        // services.AddSingleton<IMessagePublisher>(sp => environment.IsDevelopment()
        //     ? new RabbitMqPublisher("localhost")
        //     : new AzureServiceBusPublisher(connectionString));

        services.AddSingleton<IDomainEventPublisher, MessageBusDomainEventPublisher>(); 
        services.Decorate<IDomainEventPublisher, TelemetryDomainEventPublisherDecorator>();
        
        
        services.AddScoped<IDomainEventDispatcher,DomainEventDispatcher>();
        return services;
    }
}