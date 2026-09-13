using System.Globalization;
using System.Text.Json.Serialization;

namespace TemperatureApi.Models;

/// <summary>
/// Показание датчика температуры.
/// </summary>
public sealed record TemperatureReading
{
    /// <summary>Правдоподобный диапазон комнатной температуры, °C.</summary>
    private const double MinCelsius = 15.0;
    private const double MaxCelsius = 30.0;

    [JsonPropertyName("value")]
    public required double Value { get; init; }

    [JsonPropertyName("unit")]
    public required string Unit { get; init; }

    [JsonPropertyName("timestamp")]
    public required string Timestamp { get; init; }

    [JsonPropertyName("location")]
    public required string Location { get; init; }

    [JsonPropertyName("status")]
    public required string Status { get; init; }

    [JsonPropertyName("sensor_id")]
    public required string SensorId { get; init; }

    [JsonPropertyName("sensor_type")]
    public required string SensorType { get; init; }

    [JsonPropertyName("description")]
    public required string Description { get; init; }

    /// <summary>
    /// Собирает показание по одному из двух ключей, который пришёл в запросе.
    /// </summary>
    public static TemperatureReading Create(string? location, string? sensorId)
    {
        if (string.IsNullOrWhiteSpace(location))
        {
            location = sensorId switch
            {
                "1" => "Living Room",
                "2" => "Bedroom",
                "3" => "Kitchen",
                _ => "Unknown",
            };
        }

        if (string.IsNullOrWhiteSpace(sensorId))
        {
            sensorId = location switch
            {
                "Living Room" => "1",
                "Bedroom" => "2",
                "Kitchen" => "3",
                _ => "0",
            };
        }

        var value = Math.Round(
            MinCelsius + (Random.Shared.NextDouble() * (MaxCelsius - MinCelsius)),
            1);

        return new TemperatureReading
        {
            Value = value,
            Unit = "°C",
            Timestamp = DateTime.UtcNow.ToString(
                "yyyy-MM-dd'T'HH:mm:ss.fff'Z'", CultureInfo.InvariantCulture),
            Location = location,
            Status = "active",
            SensorId = sensorId,
            SensorType = "temperature",
            Description = $"Имитация показания датчика температуры, комната «{location}»",
        };
    }
}
