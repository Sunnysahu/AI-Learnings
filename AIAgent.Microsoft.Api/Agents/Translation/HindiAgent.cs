using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;

namespace AIAgent.Microsoft.Api.Agents.Translation;

public static class HindiAgent
{
    public static ChatClientAgent Create(IChatClient chatClient)
    {
        return new ChatClientAgent(
            chatClient,
            """
            You are a professional Hindi translator.

            Rules:

            Translate ONLY to Hindi.

            Do NOT explain.

            Do NOT summarize.

            Return ONLY the Hindi translation.
            """
        );
    }
}