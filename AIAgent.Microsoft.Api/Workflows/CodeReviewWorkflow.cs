using AIAgent.Microsoft.Api.Models;
using AIAgent.Microsoft.Api.Services;
using Microsoft.Agents.AI.Workflows;

namespace AIAgent.Microsoft.Api.Workflows;

public sealed class CodeReviewWorkflow
{
    private readonly IAgentFactory _factory;

    public CodeReviewWorkflow(IAgentFactory factory) => _factory = factory;

    public Workflow Build()
    {
        var architecture = _factory.CreateArchitectureAgent();
        var security = _factory.CreateSecurityAgent();
        var performance = _factory.CreatePerformanceAgent();
        var summary = _factory.CreateCodeReviewSummaryAgent();

        var approval = new ApprovalExecutor();

        ExecutorBinding architectureBinding = architecture;
        ExecutorBinding securityBinding = security;
        ExecutorBinding performanceBinding = performance;
        ExecutorBinding approvalBinding = approval;
        ExecutorBinding summaryBinding = summary;

        WorkflowBuilder builder = new(architectureBinding);

        builder.BindExecutor(architectureBinding);
        builder.BindExecutor(securityBinding);
        builder.BindExecutor(performanceBinding);
        builder.BindExecutor(approvalBinding);
        builder.BindExecutor(summaryBinding);

        builder.AddEdge<object>(architectureBinding, securityBinding, null, false);

        builder.AddEdge<object>(securityBinding, performanceBinding, null, false);

        builder.AddEdge<object>(performanceBinding, approvalBinding, null, false);

        builder.AddEdge<ApprovalResult>(
        approvalBinding,
        summaryBinding,
        result => result?.Approved == true,
        false
        );

        builder.WithOutputFrom(summaryBinding);

        return builder.Build();
    }
}