using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();

var app = builder.Build();

var upstreamUrl = builder.Configuration["UPSTREAM_URL"] ?? "http://localhost:5145/ping";

app.MapGet("/ping", async ([FromServices] IHttpClientFactory httpClientFactory) =>
{
    var httpClient = httpClientFactory.CreateClient();

    try
    {
        var response = await httpClient.GetStringAsync(upstreamUrl);

        var upstream = JsonSerializer.Deserialize<UpstreamResponse>(response, Options.JsonOptions);

        return new { Public = "Ok", Upstream = upstream?.Upstream ?? "" };
    }
    catch (Exception ex)
    {
        return new { Public = "Ok", Upstream = $"Error: {ex.Message}" };
    }
});

app.Run();

internal record UpstreamResponse(string Upstream);

internal static class Options
{
    public static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };
}