using System.Collections.Concurrent;

namespace AIAgent.Microsoft.Api.Services;

public sealed class ConversationSessionManager
{
    private readonly ConcurrentDictionary<Guid, ConversationSession> _sessions = new();

    public ConversationSession GetSession(Guid sessionId) => 
        _sessions.GetOrAdd(sessionId, id => new ConversationSession(id));

    public void Add(ConversationSession session)
    {
        _sessions.TryAdd(session.SessionId, session);
    }

    public void Remove(Guid sessionId)
    {
        _sessions.TryRemove(sessionId, out _);
    }

    //public IReadOnlyCollection<ConversationSession> GetAll()
    //{
    //    return _sessions.Values.ToList().AsReadOnly();
    //}

    public ICollection<ConversationSession> GetAll()
    {
        return _sessions.Values;
    }
}
