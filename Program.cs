// Add CORS services
using System.Text.Json;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Retrieve the production domain from environment variables
var productionDomain = builder.Configuration["PRODUCTION_DOMAIN"];
Console.WriteLine($"Current production domain is {productionDomain}");

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowedOrigins",
        policy =>
        {
            policy.SetIsOriginAllowed(origin =>
            {
                var uri = new Uri(origin);
                Console.WriteLine($"Origin: {origin}, Host: {uri.Host}");
                return uri.Host == "localhost" || uri.Host == new Uri(productionDomain).Host;
            })
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddInMemoryRateLimiting();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use the CORS policy
app.UseCors("AllowedOrigins");

app.UseIpRateLimiting();

// route maps ...

app.MapGet("/games", async ([FromQuery] string? queryParams) =>
    {
        using HttpClient client = new();

        try
        {
            var routeUrl = ApiUtils.GetApiUrl(builder.Configuration, "games", queryParams);
            var response = await client.GetAsync(routeUrl);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<object>(content);

            return Results.Ok(result);
        }
        catch (Exception exception)
        {
            Console.WriteLine("Oops! Something went wrong.");

            return Results.BadRequest(exception.Message);
        }
    })
    .WithName("GetGames")
    .WithOpenApi();

app.MapGet("/games/{id:int}", async (int id) =>
    {
        using HttpClient client = new();

        try
        {
            var routeUrl = ApiUtils.GetApiUrl(builder.Configuration, $"games/{id}");
            var response = await client.GetAsync(routeUrl);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<object>(content);

            return Results.Ok(result);
        }
        catch (Exception exception)
        {
            Console.WriteLine("Oops! Something went wrong.");

            return Results.BadRequest(exception.Message);
        }
    })
    .WithName("GetGameById")
    .WithOpenApi();

app.MapGet("/games/{id:int}/screenshots", async (int id) =>
    {
        using HttpClient client = new();

        try
        {
            var routeUrl = ApiUtils.GetApiUrl(builder.Configuration, $"games/{id}/screenshots");
            var response = await client.GetAsync(routeUrl);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<object>(content);

            return Results.Ok(result);
        }
        catch (Exception exception)
        {
            Console.WriteLine("Oops! Something went wrong.");

            return Results.BadRequest(exception.Message);
        }
    })
    .WithName("GetGameScreenshots")
    .WithOpenApi();

app.MapGet("/genres", async ([FromQuery] string? queryParams) =>
    {
        using HttpClient client = new();

        try
        {
            var routeUrl = ApiUtils.GetApiUrl(builder.Configuration, "genres", queryParams);
            var response = await client.GetAsync(routeUrl);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<object>(content);

            return Results.Ok(result);
        }
        catch (Exception exception)
        {
            Console.WriteLine("Oops! Something went wrong.");

            return Results.BadRequest(exception.Message);
        }
    })
    .WithName("GetGenres")
    .WithOpenApi();

app.MapGet("/genres/{id:int}", async (int id) =>
    {
        using HttpClient client = new();

        try
        {
            var routeUrl = ApiUtils.GetApiUrl(builder.Configuration, $"genres/{id}");
            var response = await client.GetAsync(routeUrl);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<object>(content);

            return Results.Ok(result);
        }
        catch (Exception exception)
        {
            Console.WriteLine("Oops! Something went wrong.");

            return Results.BadRequest(exception.Message);
        }
    })
    .WithName("GetGenreById")
    .WithOpenApi();

app.MapGet("/publishers", async ([FromQuery] string? queryParams) =>
    {
        using HttpClient client = new();

        try
        {
            var routeUrl = ApiUtils.GetApiUrl(builder.Configuration, "publishers", queryParams);
            var response = await client.GetAsync(routeUrl);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<object>(content);

            return Results.Ok(result);
        }
        catch (Exception exception)
        {
            Console.WriteLine("Oops! Something went wrong.");

            return Results.BadRequest(exception.Message);
        }
    })
    .WithName("GetPublishers")
    .WithOpenApi();

app.MapGet("/publishers/{id:int}", async (int id) =>
    {
        using HttpClient client = new();

        try
        {
            var routeUrl = ApiUtils.GetApiUrl(builder.Configuration, $"publishers/{id}");
            var response = await client.GetAsync(routeUrl);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<object>(content);

            return Results.Ok(result);
        }
        catch (Exception exception)
        {
            Console.WriteLine("Oops! Something went wrong.");

            return Results.BadRequest(exception.Message);
        }
    })
    .WithName("GetPublisherById")
    .WithOpenApi();

app.MapGet("/platforms", async ([FromQuery] string? queryParams) =>
    {
        using HttpClient client = new();

        try
        {
            var routeUrl = ApiUtils.GetApiUrl(builder.Configuration, "platforms", queryParams);
            var response = await client.GetAsync(routeUrl);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<object>(content);

            return Results.Ok(result);
        }
        catch (Exception exception)
        {
            Console.WriteLine("Oops! Something went wrong.");

            return Results.BadRequest(exception.Message);
        }
    })
    .WithName("GetPlatforms")
    .WithOpenApi();

app.MapGet("/platforms/{id:int}", async (int id) =>
    {
        using HttpClient client = new();

        try
        {
            var routeUrl = ApiUtils.GetApiUrl(builder.Configuration, $"platforms/{id}");
            var response = await client.GetAsync(routeUrl);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<object>(content);

            return Results.Ok(result);
        }
        catch (Exception exception)
        {
            Console.WriteLine("Oops! Something went wrong.");

            return Results.BadRequest(exception.Message);
        }
    })
    .WithName("GetPlatformById")
    .WithOpenApi();

app.MapGet("/tags", async ([FromQuery] string? queryParams) =>
    {
        using HttpClient client = new();

        try
        {
            var routeUrl = ApiUtils.GetApiUrl(builder.Configuration, "tags", queryParams);
            var response = await client.GetAsync(routeUrl);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<object>(content);

            return Results.Ok(result);
        }
        catch (Exception exception)
        {
            Console.WriteLine("Oops! Something went wrong.");

            return Results.BadRequest(exception.Message);
        }
    })
    .WithName("GetTags")
    .WithOpenApi();

app.MapGet("/tags/{id:int}", async (int id) =>
    {
        using HttpClient client = new();

        try
        {
            var routeUrl = ApiUtils.GetApiUrl(builder.Configuration, $"tags/{id}");
            var response = await client.GetAsync(routeUrl);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<object>(content);

            return Results.Ok(result);
        }
        catch (Exception exception)
        {
            Console.WriteLine("Oops! Something went wrong.");

            return Results.BadRequest(exception.Message);
        }
    })
    .WithName("GetTagById")
    .WithOpenApi();

Console.WriteLine($"Current production domain is {builder.Configuration["PRODUCTION_DOMAIN"]}");

app.Run();

internal static class ApiUtils
{
    public static string GetApiUrl(IConfiguration configuration,
        string route,
        string? queryParams = "")
    {
        var apiKey = configuration["CLIENT_SECRET"];
        return $"https://api.rawg.io/api/{route}?token&key={apiKey}{queryParams}";
    }
}
