using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;

namespace AI_Agent_Basic.Agents;

public sealed class AgentFactory
{
    private readonly IChatClient _chatClient;

    public AgentFactory(IChatClient chatClient) => _chatClient = chatClient;
    

    public AIAgent Create(AgentDefinition definition) => 
        _chatClient.AsAIAgent(definition.instruction, definition.name);
}
