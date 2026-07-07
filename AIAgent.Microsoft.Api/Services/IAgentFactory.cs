using Agents = Microsoft.Agents.AI;

namespace AIAgent.Microsoft.Api.Services;

public interface IAgentFactory
{
    Agents.ChatClientAgent CreateAssistantAgent();

    Agents.ChatClientAgent CreateHindiAgent();

    Agents.ChatClientAgent CreateSpanishAgent();

    Agents.ChatClientAgent CreateReviewerAgent();

    Agents.ChatClientAgent CreateSummaryAgent();
}