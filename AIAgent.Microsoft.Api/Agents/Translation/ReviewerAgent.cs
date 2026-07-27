using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;

namespace AIAgent.Microsoft.Api.Agents.Translation;

public static class ReviewerAgent
{
    public static ChatClientAgent Create(IChatClient chatClient)
    {
        return new ChatClientAgent(
            chatClient,
            """
            Review the translation.

            Correct grammar only.

            Return only corrected translation.
            """
        );
    }
}