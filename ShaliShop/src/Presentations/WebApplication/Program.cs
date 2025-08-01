using CatalogModule.Api;
using CatalogModule.DependencyInjection;
using CheckoutModule.DependencyInjection;
using InventoryModule.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Shared.Application.Behavior;
using Shared.Application.Events; 
using Shared.Presentation.Cors;
using Shared.Presentation.ExceptionHandling;
using Shared.Presentation.HeathCheck;
using Shared.Telemetry;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
// Configure Services.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("DefaultConnection not found");
builder.Services
    .AddCheckoutModule(connectionString)
    .AddInventoryModule(connectionString)
    .AddCatalogModule(connectionString)
    .AddTransient(typeof(IPipelineBehavior<,>), typeof(DomainExceptionPipelineBehavior<,>))
    .AddSwaggerGen(p => p.EnableAnnotations())
    .AddCorsServices()
    .AddApplicationInsightsTelemetry()
    .AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = true;
    })
    .AddHealthChecksServices()
    .AddSqlServer(connectionString, name: "SQL Server");

builder.Services.AddSingleton<IDomainEventPublisher, TelemetryDomainEventPublisher>();
builder.Services.AddScoped<DomainEventDispatcher>();

//
// builder.Logging.AddConsole();
// builder.Logging.AddDebug();
//
// var logger = builder.Logging.CreateLogger("Startup");
// logger.LogInformation("Environment: {env}", builder.Environment.EnvironmentName);
// logger.LogInformation("Modules loaded: Catalog, Inventory, Checkout");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DocumentTitle = "ShaliShop API";
        options.RoutePrefix = "docs";
    });

    app.MapOpenApi();
}
app.UseHttpsRedirection();
app.UseCorrelationId();

if (!app.Environment.IsDevelopment())
{
    app.UseCustomExceptionHandler();
}

app.MapCatalogEndpoints();
app.UseCustomHealthChecks(pageTitle: "Shali Shop Health check.");
app.MapCustomHealthChecks();
app.UseCustomCors();

app.Run();