namespace AI_Translation_Agent.Agents.Definitions;

public interface IAgentDefinitionProvider
{
    AgentDefinition Hindi();

    AgentDefinition Spanish();

    AgentDefinition Reviewer();

    AgentDefinition Summary();
}