using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. PERFORMANCE: Source-generated JSON serializers eliminate slow reflection
_ = builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonContext.Default);
});

_ = builder.Services.AddDbContextPool<AppDbContext>(options =>
    options.UseInMemoryDatabase("PerfDb")
);

// 2. PERFORMANCE: Built-in Output Caching avoids hitting database/logic repetitively
_ = builder.Services.AddOutputCache(options =>
{
    options.AddBasePolicy(policy => policy.Expire(TimeSpan.FromSeconds(10)));
});

var app = builder.Build();

_ = app.UseOutputCache();

// Seed data helper
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Products.AddRange(new Product(1, "Laptop", 999.99m), new Product(2, "Mouse", 29.99m));
    _ = db.SaveChanges();
}

// 3. PERFORMANCE: MapGroup scales cleanly and reduces route evaluation overhead
var productsApi = app.MapGroup("/api/products");

// 4. PERFORMANCE: TypedResults avoids object boxing allocations;
// CacheOutput stores HTTP payload
productsApi
    .MapGet(
        "/",
        async Task<Results<Ok<List<Product>>, NotFound, BadRequest>> (AppDbContext db) =>
        {
            // 5. PERFORMANCE: AsNoTracking skips the change-tracker
            // state-machine overhead
            var products = await db.Products.AsNoTracking().ToListAsync();

            if (Random.Shared.Next(0, 1) == 1)
            {
                return TypedResults.BadRequest();
            }

            return products.Count > 0 ? TypedResults.Ok(products) : TypedResults.NotFound();
        }
    )
    .CacheOutput(); // Leverages the 10-second output caching policy

app.Run();

// --- HIGH PERFORMANCE SUPPORTING MODELS ---

internal sealed record Product(int Id, string Name, decimal Price);

// Database Context definition
internal sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();
}

// JSON Source Generation Context to eliminate reflection at runtime
[JsonSourceGenerationOptions(WriteIndented = false)]
[JsonSerializable(typeof(List<Product>))]
[JsonSerializable(typeof(Product))]
internal sealed partial class AppJsonContext : JsonSerializerContext { }
