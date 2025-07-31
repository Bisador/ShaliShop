using CatalogModule.Api;
using CatalogModule.DependencyInjection;
using CheckoutModule.DependencyInjection;
using InventoryModule.DependencyInjection;
using Shared.Application.Behavior;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("DefaultConnection not found");
builder.Services
    .AddCheckoutModule(connectionString)
    .AddInventoryModule(connectionString)
    .AddCatalogModule(connectionString)
    .AddTransient(typeof(IPipelineBehavior<,>), typeof(DomainExceptionPipelineBehavior<,>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapCatalogEndpoints();

app.Run();