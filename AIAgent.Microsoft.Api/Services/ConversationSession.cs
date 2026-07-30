using Microsoft.Extensions.AI;

namespace AIAgent.Microsoft.Api.Services;

public sealed class ConversationSession
{
    public Guid Id { get; }

    public List<ChatMessage> Messages { get; } = [];

    public ConversationSession(Guid id)
    {
        Id = id;
    }
}
