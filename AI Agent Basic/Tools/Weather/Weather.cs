using Microsoft.Extensions.AI;
using System.Text.Json.Serialization;

namespace AI_Agent_Basic.Tools.Weather;

public static class Weather
{
    private static readonly HttpClient HttpClient = new();

    public static readonly AITool GetWeather =
        AIFunctionFactory.Create(async ( string city, CancellationToken cancellationToken) =>
            {
                return await GetWeatherAsync(city, cancellationToken);
            },
            name: "get_weather",
            description:
            """
               Gets the current weather for a city.

             Returns:
             - Temperature in Celsius (°C)
             - Humidity in percent (%)
             - Wind speed in kilometers per hour (km/h)
             - Current weather condition

             Always use this tool when the user asks
             about current weather.
             """
        );


    private static async Task<WeatherResult> GetWeatherAsync(string city, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Weather tool called for: {city}");

        // Step 1: Find the city coordinates

        var geocodingUrl =
            "https://geocoding-api.open-meteo.com/v1/search" +
            $"?name={Uri.EscapeDataString(city)}" +
            "&count=1" +
            "&language=en" +
            "&format=json";

        var locationResponse =
            await HttpClient.GetFromJsonAsync<GeocodingResponse>(
                geocodingUrl,
                cancellationToken);

        var location =
            locationResponse?.Results?.FirstOrDefault();

        if (location is null)
        {
            throw new InvalidOperationException($"City '{city}' was not found.");
        }


        // Step 2: Call weather API

        var weatherUrl =
            "https://api.open-meteo.com/v1/forecast" +
            $"?latitude={location.Latitude}" +
            $"&longitude={location.Longitude}" +
            "&current=temperature_2m," +
            "relative_humidity_2m," +
            "weather_code," +
            "wind_speed_10m";

        var weatherResponse =
            await HttpClient.GetFromJsonAsync<WeatherApiResponse>(weatherUrl, cancellationToken);

        if (weatherResponse?.Current is null)
        {
            throw new InvalidOperationException($"Weather information unavailable for '{city}'.");
        }


        // Step 3: Return result

        return new WeatherResult
        {
            City = location.Name,

            Temperature = weatherResponse.Current.Temperature,

            Condition = GetCondition(weatherResponse.Current.WeatherCode),

            Humidity = weatherResponse.Current.Humidity,

            WindSpeed = weatherResponse.Current.WindSpeed
        };
    }


    private static string GetCondition(int code)
    {
        return code switch
        {
            0 => "Clear sky",

            1 or 2 or 3 => "Partly cloudy",

            45 or 48 => "Fog",

            51 or 53 or 55 or 56 or 57 => "Drizzle",

            61 or 63 or 65 or 66 or 67 => "Rain",

            71 or 73 or 75 or 77 => "Snow",

            80 or 81 or 82 => "Rain showers",

            85 or 86 => "Snow showers",

            95 or 96 or 99 => "Thunderstorm",

            _ => "Unknown"
        };
    }


    private sealed class GeocodingResponse
    {
        public List<Location>? Results { get; set; }
    }


    private sealed class Location
    {
        public string Name { get; set; } = string.Empty;

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }


    private sealed class WeatherApiResponse
    {
        public CurrentWeather? Current { get; set; }
    }


    private sealed class CurrentWeather
    {
        [JsonPropertyName("temperature_2m")]
        public double Temperature { get; set; }

        [JsonPropertyName("relative_humidity_2m")]
        public double Humidity { get; set; }

        [JsonPropertyName("weather_code")]
        public int WeatherCode { get; set; }

        [JsonPropertyName("wind_speed_10m")]
        public double WindSpeed { get; set; }
    }
}