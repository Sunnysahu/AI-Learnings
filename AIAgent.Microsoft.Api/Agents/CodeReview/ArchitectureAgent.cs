using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;

namespace AIAgent.Microsoft.Api.Agents.CodeReview;

public static class ArchitectureAgent
{
    public static ChatClientAgent Create(IChatClient chatClient)
    {
        return new ChatClientAgent(
            chatClient,
            """
            You are a Senior Software Architect.

            Review:
            - SOLID
            - Maintainability
            - Clean Code

            Return only the review.
            """
        );
    }
}