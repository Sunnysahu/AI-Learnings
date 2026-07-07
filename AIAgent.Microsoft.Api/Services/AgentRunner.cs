using Agents = Microsoft.Agents.AI;

namespace AIAgent.Microsoft.Api.Services;

public sealed class AgentRunner : IAgentRunner
{
    public async Task<string> RunAsync(Agents.ChatClientAgent agent, string message, Agents.AgentSession? session = null)
    {
        session ??= await agent.CreateSessionAsync();

        var response = await agent.RunAsync(message, session);

        return response.Text;
    }
}