using AIAgent.Microsoft.Api.Services;
using Microsoft.Agents.AI;
using Microsoft.Agents.AI.Workflows;

namespace AIAgent.Microsoft.Api.Workflows;

public sealed class TranslationWorkflow
{
    private readonly IAgentFactory _factory;

    public TranslationWorkflow(IAgentFactory factory)
    {
        _factory = factory;
    }

    public Workflow Build()
    {
         
        var agents = new[] 
        { 
            _factory.CreateHindiAgent(), 
            _factory.CreateReviewerAgent(), 
            _factory.CreateSummaryAgent()
        };

        return AgentWorkflowBuilder.BuildSequential(
            "Translation Workflow",
            agents
        );
    }
}