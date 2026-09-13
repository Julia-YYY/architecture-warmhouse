using TemperatureApi.Models;

// temperature-api — заглушка удалённого датчика температуры.
// Состояния не хранит: отвечает на любой идентификатор, который видит впервые.

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/health", () => Results.Ok(new { status = "ok" }));

// GET /temperature?location=...
// Вызывается из smart_home/services/temperature_service.go:40 (GetTemperature).
app.MapGet("/temperature", (string? location) =>
{
    if (string.IsNullOrWhiteSpace(location))
    {
        return Results.BadRequest(new { error = "query parameter 'location' is required" });
    }

    return Results.Ok(TemperatureReading.Create(location, sensorId: null));
});

// GET /temperature/{sensorId}
// Вызывается из smart_home/services/temperature_service.go:62 (GetTemperatureByID).
app.MapGet("/temperature/{sensorId}", (string sensorId) =>
    Results.Ok(TemperatureReading.Create(location: null, sensorId: sensorId)));

app.Run();
