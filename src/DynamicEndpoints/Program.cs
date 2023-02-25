var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

var endpoints = new[] { "Abobrinha", "Xpto", "galinha" };

foreach (var endpoint in endpoints)
    app.MapGet($"/{endpoint}", () => $"Call {endpoint}");

app.Run();
