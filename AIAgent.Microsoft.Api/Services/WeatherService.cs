using AIAgent.Microsoft.Api.Models;
using System.Text.Json;

namespace AIAgent.Microsoft.Api.Services;

public sealed class WeatherService
{
    private readonly HttpClient _httpClient;

    public WeatherService(HttpClient httpClient) => _httpClient = httpClient;

    public async Task<string> GetWeatherAsync(string city)
    {
        city = city.ToLower();

        (double lat, double lon) = city switch
        {
            "mumbai" => (19.0760, 72.8777),
            "delhi" => (28.6139, 77.2090),
            "kolkata" => (22.5726, 88.3639),
            "bangalore" => (12.9716, 77.5946),
            "chennai" => (13.0827, 80.2707),
            _ => throw new Exception($"Unknown city: {city}")
        };

        string url = $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&current=temperature_2m,weather_code";

        string json = await _httpClient.GetStringAsync(url);

        WeatherResponse? weather = JsonSerializer.Deserialize<WeatherResponse>(json);

        if (weather == null) return "Unable to fetch weather.";

        return $"{city} temperature is {weather.Current.Temperature}°C";
    }
}