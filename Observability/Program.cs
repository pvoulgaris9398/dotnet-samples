using Serilog;

#pragma warning disable CA1305 // Specify IFormatProvider
Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateBootstrapLogger();
#pragma warning restore CA1305 // Specify IFormatProvider

try
{
    Log.Information("Starting web application...");
    var builder = WebApplication.CreateBuilder(args);

    // 2. Wire up Serilog into the DI container
    _ = builder.Services.AddSerilog(
        (services, lc) =>
            lc.ReadFrom.Configuration(builder.Configuration).ReadFrom.Services(services)
    );

    var app = builder.Build();

    var summaries = new[]
    {
        "Freezing",
        "Bracing",
        "Chilly",
        "Cool",
        "Mild",
        "Warm",
        "Balmy",
        "Hot",
        "Sweltering",
        "Scorching",
    };

    _ = app.MapGet(
            "/weatherforecast",
            () =>
            {
                var forecast = Enumerable
                    .Range(1, 5)
                    .Select(index => new WeatherForecast(
                        DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                        Random.Shared.Next(-20, 55),
                        summaries[Random.Shared.Next(summaries.Length)]
                    ))
                    .ToArray();
                return forecast;
            }
        )
        .WithName("GetWeatherForecast");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}

internal sealed record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
