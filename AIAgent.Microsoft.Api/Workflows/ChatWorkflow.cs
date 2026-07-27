using AIAgent.Microsoft.Api.Services;
using Microsoft.Agents.AI.Workflows;

namespace AIAgent.Microsoft.Api.Workflows;

public sealed class ChatWorkflow
{
    private readonly IAgentFactory _factory;

    public ChatWorkflow(IAgentFactory factory) => _factory = factory;

    public Workflow Build()
    {
        return AgentWorkflowBuilder.BuildSequential(_factory.CreateAssistantAgent());
    }
}