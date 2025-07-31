using CheckoutModule.Persistence.DependencyInjection;
using WebApplication;

var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("DefaultConnection not found");
builder.Services.AddCheckoutModulePersistence(connectionString);
// builder.Services.RegisterShopApplicationLayer();
// builder.Services.AddMediatR(cfg =>
// {
//     cfg.RegisterServicesFromAssemblies(
//         typeof(OrderPlaceCommandHandler).Assembly
//     );
// });
// builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(DomainExceptionPipelineBehavior<,>)); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};
// app.MapGet("/PlaceOrder", async ([FromServices] IMediator mediator) =>
//     {
//         var result = await mediator.Send(new OrderPlaceCommand(Guid.NewGuid(), new ShippingAddressDto()
//         {
//             City = "",
//             Street = "",
//             ZipCode = "",
//         }));
//         return result.IsSuccess ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest;
//     })
//     .WithName("PlaceOrder");

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast");

app.Run();

namespace WebApplication
{
    record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
    {
        public int TemperatureF => 32 + (int) (TemperatureC / 0.5556);
    }
}