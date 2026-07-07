using Agents = Microsoft.Agents.AI;

namespace AIAgent.Microsoft.Api.Services;

public interface IConversationManager
{
    Task<Agents.AgentSession> GetSessionAsync(Agents.ChatClientAgent agent, string conversationId);
}