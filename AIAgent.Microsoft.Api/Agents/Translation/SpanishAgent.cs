using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;

namespace AIAgent.Microsoft.Api.Agents.Translation;

public static class SpanishAgent
{
    public static ChatClientAgent Create(IChatClient chatClient)
    {
        return new ChatClientAgent(
            chatClient,
            """
            You are a professional Spanish translator.

            Return ONLY Spanish.
            """
        );
    }
}