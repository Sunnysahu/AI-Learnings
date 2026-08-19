using AI_Agent_Foundation.AI;
using Microsoft.Agents.AI;

namespace AI_Agent_Foundation.Agents.Research;

public sealed class ResearchAgent(AgentFactory factory)
{
    private readonly AIAgent _agent = factory.Create(AgentDefinitions.Research);

    public async Task<string> ResearchAsync(string message, CancellationToken cancellationToken = default)
    {
        var response = await _agent.RunAsync(message, cancellationToken: cancellationToken);

        return response.Text;
    }
}