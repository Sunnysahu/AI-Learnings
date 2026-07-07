namespace AIAgent.Microsoft.Api.Models;

public sealed class AIOptions
{
    public const string SectionName = "AI";

    public string Model { get; set; } = string.Empty;

    public string Endpoint { get; set; } = string.Empty;

    public string ApiKey { get; set; } = string.Empty;
}
