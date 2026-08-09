using Microsoft.Extensions.AI;

namespace AIAgent.Microsoft.Api.Services;

public sealed class ConversationSession
{
    public Guid SessionId { get; }

    public List<ChatMessage> Messages { get; } = [];

    public ConversationSession(Guid sessionId)
    {
        SessionId = sessionId;
    }
}
