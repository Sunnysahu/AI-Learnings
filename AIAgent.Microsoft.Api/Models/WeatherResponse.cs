using System.Text.Json.Serialization;

namespace AIAgent.Microsoft.Api.Models;

public sealed class WeatherResponse
{
    [JsonPropertyName("current")]
    public CurrentWeather Current { get; set; } = new();
}

public sealed class CurrentWeather
{
    [JsonPropertyName("temperature_2m")]
    public double Temperature { get; set; }

    [JsonPropertyName("weather_code")]
    public int WeatherCode { get; set; }
}
