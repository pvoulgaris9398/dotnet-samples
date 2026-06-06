using Serilog;

#pragma warning disable CA1305 // Specify IFormatProvider
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();
#pragma warning restore CA1305 // Specify IFormatProvider

try
{
    Log.Information("Starting web application...");
    var builder = WebApplication.CreateBuilder(args);

    _ = builder.Host.UseSerilog();

    // 2. Wire up Serilog into the DI container
    _ = builder.Services.AddSerilog(
        (services, lc) =>
            lc.ReadFrom.Configuration(builder.Configuration).ReadFrom.Services(services)
    );

    var app = builder.Build();

    // Optional: Add request logging to see HTTP requests in the console
    _ = app.UseSerilogRequestLogging();

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

    /*
        _ = app.MapGet(
                "/prices",
                async IAsyncEnumerable<Price> (Serilog.ILogger logger) =>
                {
                    await Task.Delay(10);
                    yield return new(new("EUR"), 100);
                    break;
                }
            )
            .WithName("PriceEndpoint");
            */

    _ = app.MapGet(
            "/weatherforecast",
            async (Serilog.ILogger logger) =>
            {
                int pause = Random.Shared.Next(3, 10);

                await Task.Delay(pause * 1000);

                logger.Information("Testing: {CurrentTime}", DateTime.UtcNow);

                var forecast = Enumerable
                    .Range(1, 5)
                    .Select(index => new WeatherForecast(
                        DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                        Random.Shared.Next(-20, 55),
                        summaries[Random.Shared.Next(summaries.Length)]
                    ))
                    .ToArray();

                return TypedResults.Ok(forecast);
            }
        )
        .WithName("GetWeatherForecast");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

internal sealed record Currency(string Code);

internal sealed record Price(Currency Currency, decimal Value);

internal sealed record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
