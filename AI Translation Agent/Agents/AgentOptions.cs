namespace AI_Translation_Agent.Agents;

public sealed class AgentOptions
{
    public required string Name { get; init; }

    public required string Description { get; init; }

    public required string Instructions { get; init; }

    public required AgentAction Action { get; init; }
}