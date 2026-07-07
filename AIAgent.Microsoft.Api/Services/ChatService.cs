using Microsoft.Extensions.AI;

using AI = Microsoft.Extensions.AI;
using Agents = Microsoft.Agents.AI;
using OpenAIClient = OpenAI;

namespace AIAgent.Microsoft.Api.Services;

public sealed class ChatService : IChatService
{
    private readonly AI.IChatClient _chatClient;

    public ChatService(IChatClient chatClient) => _chatClient = chatClient;

    public async Task<string> AskAsync(string message)
    {
        var response = await _chatClient.GetResponseAsync([
            new AI.ChatMessage(AI.ChatRole.User, message)
        ]);

        return response.Text ?? string.Empty;
    }
}
