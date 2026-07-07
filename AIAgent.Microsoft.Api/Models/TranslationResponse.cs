namespace AIAgent.Microsoft.Api.Models;

public sealed class TranslationResponse
{
    public string Original { get; set; } = string.Empty;

    public string Hindi { get; set; } = string.Empty;

    public string Spanish { get; set; } = string.Empty;

    public string Reviewed { get; set; } = string.Empty;

    public string Summary { get; set; } = string.Empty;
}