using AI_Agent_Foundation.Agents;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;

namespace AI_Agent_Foundation.AI;

public sealed class AgentFactory(IChatClient chatClient)
{
    public AIAgent Create(AgentDefinition definition)
    {
        return new ChatClientAgent(
            chatClient, 
            definition.Instructions, 
            definition.Name, 
            definition.Description
        );
    }
}