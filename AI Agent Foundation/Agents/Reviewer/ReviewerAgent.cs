using AI_Agent_Foundation.AI;
using Microsoft.Agents.AI;

namespace AI_Agent_Foundation.Agents.Reviewer;

public sealed class ReviewerAgent(AgentFactory factory)
{
    private readonly AIAgent _agent = factory.Create(AgentDefinitions.Reviewer);

    public async Task<string> ReviewAsync(string content, CancellationToken cancellationToken = default)
    {
        var response = await _agent.RunAsync(content, cancellationToken: cancellationToken);

        return response.Text;
    }
}