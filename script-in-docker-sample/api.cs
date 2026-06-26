#!/usr/bin/env -S dotnet run

// 1. Tell the .NET CLI to treat this file as a Web Application
#:sdk Microsoft.NET.Sdk.Web

// 2. Disable Native AOT (recommended for quick scripting/reflection compatibility)
#:property PublishAot=false

// 3. KILL THE MAGIC: Disable all hidden global using statements
#:property ImplicitUsings=disable

using System; // for DateTime, same as above
using System.Threading.Channels;
using System.Threading.Tasks; // for Task.Delay and Task.Run, same as above
// 4. Explicitly include the namespaces we need, without line 3 above they would all be implicitly included in the global usings, but we disabled that
using Microsoft.AspNetCore.Builder; // For WebApplication <= without line 3 these are _implicitly_ included in the global usings, but we disabled that
using Microsoft.AspNetCore.Http; // same as above
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection; // AddScoped
using System.Text.Json.Serialization; // JsonSerializable

namespace MySampleApp;

// 1. Define your data model
public record UserPayload(string UserName, string Role);

// 1. Existing registration for reading/writing standard user JSON
[JsonSerializable(typeof(UserPayload))]

// 2. CREATE THE CODE GENERATOR CONTEXT:
// The [JsonSerializable] attribute instructs the compiler to generate 
// serialization/deserialization logic for this type at compile time.
[JsonSerializable(typeof(UserPayload))]
// 2. THE FIX: Register the streaming interface so the code generator can process it
[JsonSerializable(typeof(System.Collections.Generic.IAsyncEnumerable<string>))]
public partial class MyJsonContext : JsonSerializerContext
{
}



// A completely custom, explicit service interface
public interface IGreetService
{
    string GetMessage();
}

// The concrete implementation of our service
public class GreetService : IGreetService
{
    public string GetMessage() => "Hello from an explicitly injected service!";
}

public class Program
{
    public static async Task Main(string[] args)
    {

        // 1. Capture and inspect your command-line arguments explicitly
        Console.WriteLine($"Arguments received: {args.Length}");
        foreach (var arg in args)
        {
            Console.WriteLine($" -> Arg: {arg}");
        }

        WebApplicationBuilder builder = WebApplication.CreateBuilder();

        // 3. REGISTER CONTEXT GLOBALLY:
        // Tell the Minimal API framework to use your source-generated code 
        // for all HTTP requests and responses, turning off reflection lookups.
        _ = builder.Services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.TypeInfoResolver = MyJsonContext.Default;
        });


        // 2. OVERRIDE THE PORT: Tell the internal Kestrel web server exactly where to listen
        // This replaces the default port (5000) with port 8080 explicitly
        _ = builder.WebHost.ConfigureKestrel(options =>
        {
            options.ListenAnyIP(8080);
        });

        // 3. DEPENDENCY INJECTION: Register your custom service into the IoC container
        _ = builder.Services.AddScoped<IGreetService, GreetService>();

        var app = builder.Build();

        // 4. TEST DESERIALIZATION (Reading Request JSON reflection-free)
        _ = app.MapPost("/user", async (HttpContext context) =>
        {

            try
            {

                // 1. Check Content-Length header first (if provided by client)
                if (context.Request.ContentLength == 0)
                {
                    return Results.Ok("Test-test-test!!!");
                    //return Results.BadRequest("Missing request body: Payload is completely empty.");
                }

                // Read the incoming JSON body using the generated static metadata
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                var data = await context.Request.ReadFromJsonAsync<UserPayload>(MyJsonContext.Default.UserPayload);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

                // 3. Check if body was present but parsed to null or empty properties
                if (data == null || string.IsNullOrWhiteSpace(data.UserName))
                {
                    return Results.BadRequest("Invalid payload: Object fields are empty.");
                }

                if (data == null) return Results.BadRequest("Invalid payload");

                Console.WriteLine($"Parsed User safely: {data.UserName} ({data.Role})");
                return Results.Ok(data);


            }
            catch (Exception)
            {
                return Results.Ok("Got an error!");
            }
        });

        // 5. TEST SERIALIZATION (Writing Response JSON reflection-free)
        _ = app.MapGet("/user/sample", () =>
        {
            var sample = new UserPayload("ExplicitDev", "Admin");

            // Results.Json accepts our compiled type metadata directly
            return Results.Json(sample, MyJsonContext.Default.UserPayload);
        });

        // _ = app.MapGet("/", () => "Hello World!");

        // 4. SERVICE RESOLUTION: Request your service directly inside an endpoint map
        _ = app.MapGet("/", (IGreetService greeter) =>
        {
            return greeter.GetMessage();
        });

        _ = app.MapGet("/stream-data", (HttpContext context) =>
        {
            var channel = Channel.CreateBounded<string>(10);

            // Populate the channel on a background thread
            _ = Task.Run(async () =>
            {
                for (int i = 1; i <= 5; i++)
                {
                    await Task.Delay(1000); // Simulate data generation delay
                    await channel.Writer.WriteAsync($"Data packet {i} at {DateTime.Now:T}");
                }
                channel.Writer.Complete(); // Signal streaming is done
            });

            // .NET 8/9 automatically serializes IAsyncEnumerable out to the HTTP response stream
            return channel.Reader.ReadAllAsync(context.RequestAborted);
        });

        await app.RunAsync();
    }
}
