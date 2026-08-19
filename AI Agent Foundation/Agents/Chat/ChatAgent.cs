using AI_Agent_Foundation.AI;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;

namespace AI_Agent_Foundation.Agents.Chat;

public sealed class ChatAgent(AgentFactory factory)
{
    private readonly AIAgent _agent = factory.Create(AgentDefinitions.Chat);

    public async Task<string> ChatAsync(string message, CancellationToken cancellationToken = default)
    {
        var response = await _agent.RunAsync(message, cancellationToken: cancellationToken);

        return response.Text;
    }
}