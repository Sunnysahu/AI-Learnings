namespace AI_Agent_Basic.Tools.Weather;

public sealed class WeatherResult
{
    public string City { get; init; } = string.Empty;

    public double Temperature { get; init; }

    public string Condition { get; init; } = string.Empty;

    public double Humidity { get; init; }

    public double WindSpeed { get; init; }
}