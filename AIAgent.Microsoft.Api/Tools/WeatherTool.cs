using AIAgent.Microsoft.Api.Services;

namespace AIAgent.Microsoft.Api.Tools;

public sealed class WeatherTool
{
    private readonly WeatherService _weatherService;

    public WeatherTool(WeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    public async Task<string> GetWeather(string city)
    {
        return await _weatherService.GetWeatherAsync(city);
    }
}