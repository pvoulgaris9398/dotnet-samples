using Serilog;

#pragma warning disable CA1305 // Specify IFormatProvider
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .Enrich.FromLogContext()
    .WriteTo.Console(
        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] ({SourceContext}) {Message:lj}{NewLine}{Exception}"
    )
    //.WriteTo.Console(new CompactJsonFormatter())
    .CreateLogger();
#pragma warning restore CA1305 // Specify IFormatProvider

try
{
    Log.Information("Starting web application...");
    var builder = WebApplication.CreateBuilder(args);

    //builder.Host.UseRequestContextLogging();

    //_ = builder.Host.UseSerilog();

    // 2. Wire up Serilog into the DI container
    // Apply the template globally to the host

    _ = builder.Host.UseSerilog(
        (context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration)
    );

    var app = builder.Build();

    _ = app.Use(
        async (context, next) =>
        {
            Log.Information("BEFORE");
            await next(context);
            Log.Information("AFTER");
        }
    );

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
            async (HttpRequest request, Serilog.ILogger logger) =>
            {
                int pause = Random.Shared.Next(0, 6);

                logger.Debug("Waiting for: {Pause} seconds...", pause);

                await Task.Delay(pause * 1000);

                logger.Information("Requested Route: {RequestedRoute}", request.Path);

                var forecast = Enumerable
                    .Range(1, 5)
                    .Select(index => new WeatherForecast(
                        DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                        Random.Shared.Next(-20, 55),
                        summaries[Random.Shared.Next(summaries.Length)]
                    ))
                    .ToArray();

                return TypedResults.Accepted(request.Path, forecast);
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
