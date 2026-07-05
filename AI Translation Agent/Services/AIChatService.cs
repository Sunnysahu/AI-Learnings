using Microsoft.Extensions.AI;

namespace AI_Translation_Agent.Services;

public class AIChatService : IAIChatService
{
    private readonly IChatClient _chatClient;

    public AIChatService(IChatClient chatClient) => _chatClient = chatClient;

    public async Task<string> AskAsync(string prompt)
    {
        ChatResponse response = await _chatClient.GetResponseAsync(prompt);

        return response.Text;
    }
}