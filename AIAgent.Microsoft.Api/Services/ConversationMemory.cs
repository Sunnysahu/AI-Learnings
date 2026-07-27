using Microsoft.Extensions.AI;

namespace AIAgent.Microsoft.Api.Services;

public sealed class ConversationMemory
{
    private readonly List<ChatMessage> _messages = [];

    public IReadOnlyList<ChatMessage> Messages => _messages;

    public void AddUserMessage(string text) => _messages.Add(new ChatMessage(ChatRole.User, text));

    public void AddAssistantMessage(string text) => _messages.Add(new ChatMessage(ChatRole.Assistant, text));

    public void Clear() => _messages.Clear();
}