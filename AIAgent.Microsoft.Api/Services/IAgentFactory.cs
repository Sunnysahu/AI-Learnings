using Microsoft.Agents.AI;

namespace AIAgent.Microsoft.Api.Services;

public interface IAgentFactory
{
    ChatClientAgent CreateAssistantAgent();

    ChatClientAgent CreateHindiAgent();

    ChatClientAgent CreateSpanishAgent();

    ChatClientAgent CreateReviewerAgent();

    ChatClientAgent CreateSummaryAgent();

    ChatClientAgent CreateArchitectureAgent();

    ChatClientAgent CreateSecurityAgent();

    ChatClientAgent CreatePerformanceAgent();

    ChatClientAgent CreateCodeReviewSummaryAgent();
}