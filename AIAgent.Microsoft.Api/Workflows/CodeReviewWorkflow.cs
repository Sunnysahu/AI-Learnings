using AIAgent.Microsoft.Api.Services;
using Microsoft.Agents.AI.Workflows;

namespace AIAgent.Microsoft.Api.Workflows;

public sealed class CodeReviewWorkflow
{
    private readonly IAgentFactory _factory;

    public CodeReviewWorkflow(IAgentFactory factory) => _factory = factory;

    public Workflow Build()
    {
        var agents = new[]
        {
            _factory.CreateArchitectureAgent(),
            _factory.CreateSecurityAgent(),
            _factory.CreatePerformanceAgent(),
            _factory.CreateCodeReviewSummaryAgent()
        };

        return AgentWorkflowBuilder.BuildSequential("Code Review Workflow", agents);
    }
}