namespace AI_Agent_Foundation.AI;

public sealed class AIOptions
{
    public const string SectionName = "AI";

    public required string Endpoint { get; init; }
    public required string ApiKey { get; init; }
    public required string Model { get; init; }
}