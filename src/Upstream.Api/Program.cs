var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/ping", () => new { Upstream = "Ok" });

app.Run();