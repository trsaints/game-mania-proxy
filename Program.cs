using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/games", async () =>
    {
        using HttpClient client = new();

        try
        {
            var routeUrl = ApiUtils.GetApiUrl(builder.Configuration, "games");
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
    .WithName("GetGames");

app.Run();

internal static class ApiUtils
{
    public static string GetApiUrl(IConfiguration configuration, string route)
    {
        var apiKey = configuration["CLIENT_SECRET"];
        return $"https://api.rawg.io/api/{route}?token&key={apiKey}";
    }
}