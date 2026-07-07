using Agents = Microsoft.Agents.AI;

namespace AIAgent.Microsoft.Api.Services;

public sealed class ConversationManager : IConversationManager
{
    private readonly Dictionary<string, Agents.AgentSession> _sessions = new();

    public async Task<Agents.AgentSession> GetSessionAsync(Agents.ChatClientAgent agent, string conversationId)
    {
        if (_sessions.TryGetValue(conversationId, out var session))
        
        return session;

        session = await agent.CreateSessionAsync();

        _sessions[conversationId] = session;

        return session;
    }
}