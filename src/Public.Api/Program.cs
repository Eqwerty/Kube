var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/ping", () => new { Public = "Ok" });

app.Run();