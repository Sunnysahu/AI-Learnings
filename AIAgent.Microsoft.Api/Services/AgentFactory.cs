using AIAgent.Microsoft.Api.Agents.Assistant;
using AIAgent.Microsoft.Api.Agents.CodeReview;
using AIAgent.Microsoft.Api.Agents.Translation;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;


namespace AIAgent.Microsoft.Api.Services;

public sealed class AgentFactory : IAgentFactory
{
    private readonly IChatClient _chatClient;
    private readonly IServiceProvider _serviceProvider;

    public AgentFactory(IChatClient chatClient, IServiceProvider serviceProvider)
    {
        _chatClient = chatClient;
        _serviceProvider = serviceProvider;
    }

    public ChatClientAgent CreateAssistantAgent() => AssistantAgent.Create(_chatClient, _serviceProvider);

    public ChatClientAgent CreateHindiAgent() => HindiAgent.Create(_chatClient);

    public ChatClientAgent CreateSpanishAgent() => SpanishAgent.Create(_chatClient);

    public ChatClientAgent CreateReviewerAgent() => ReviewerAgent.Create(_chatClient);

    public ChatClientAgent CreateSummaryAgent() => SummaryAgent.Create(_chatClient);

    public ChatClientAgent CreateArchitectureAgent() => ArchitectureAgent.Create(_chatClient);

    public ChatClientAgent CreateSecurityAgent() => SecurityAgent.Create(_chatClient);

    public ChatClientAgent CreatePerformanceAgent() => PerformanceAgent.Create(_chatClient);

    public ChatClientAgent CreateCodeReviewSummaryAgent() => CodeReviewSummaryAgent.Create(_chatClient);
}