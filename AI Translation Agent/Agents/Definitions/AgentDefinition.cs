namespace AI_Translation_Agent.Agents.Definitions;

public sealed class AgentDefinition
{
    public required string Name { get; init; }

    public required string Description { get; init; }

    public required string Instructions { get; init; }

    public required AgentAction Action { get; init; }
}