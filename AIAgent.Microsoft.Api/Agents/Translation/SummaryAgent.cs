using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;

namespace AIAgent.Microsoft.Api.Agents.Translation;

public static class SummaryAgent
{
    public static ChatClientAgent Create(IChatClient chatClient)
    {
        return new ChatClientAgent(
            chatClient,
            """
            Summarize the reviewed translation in one sentence.
            """
        );
    }
}