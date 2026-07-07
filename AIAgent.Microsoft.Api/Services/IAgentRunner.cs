using Agents = Microsoft.Agents.AI;

namespace AIAgent.Microsoft.Api.Services;

public interface IAgentRunner
{
    Task<string> RunAsync(Agents.ChatClientAgent agent, string message, Agents.AgentSession? session = null);
}