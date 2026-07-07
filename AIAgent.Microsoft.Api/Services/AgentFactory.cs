using AIAgent.Microsoft.Api.Tools;
using Agents = Microsoft.Agents.AI;
using AI = Microsoft.Extensions.AI;

namespace AIAgent.Microsoft.Api.Services;

public sealed class AgentFactory : IAgentFactory
{
    private readonly AI.IChatClient _chatClient;

    public AgentFactory(AI.IChatClient chatClient)
    {
        _chatClient = chatClient;
    }

    IList<AI.AITool> tools = [
        AI.AIFunctionFactory.Create(CalculatorTools.Add),
        AI.AIFunctionFactory.Create(CalculatorTools.Multiply)
    ];

    public Agents.ChatClientAgent CreateAssistantAgent()
    {
        return new Agents.ChatClientAgent(
            chatClient: _chatClient,
            instructions:
            """
            You are a helpful assistant.

            If a mathematical calculation is required,
            ALWAYS use the provided tools.

            Never calculate yourself.
            """,

            name: "Assistant Agent",
            description: "General Assistant",
            tools: tools
        );
    }

    public Agents.ChatClientAgent CreateHindiAgent()
    {
        return new Agents.ChatClientAgent(
            _chatClient,
            instructions:
            """
            Translate the given English text into Hindi.
            Return ONLY the Hindi translation.
            """,
            name: "Hindi Agent",
            description: "Hindi Translator");
    }

    public Agents.ChatClientAgent CreateSpanishAgent()
    {
        return new Agents.ChatClientAgent(
            _chatClient,
            instructions:
            """
            Translate the given English text into Spanish.
            Return ONLY the Spanish translation.
            """,
            name: "Spanish Agent",
            description: "Spanish Translator"
        );
    }

    public Agents.ChatClientAgent CreateReviewerAgent()
    {
        return new Agents.ChatClientAgent(
            _chatClient,
            instructions:
            """
            Review the given translation.
            Correct grammar, spelling and meaning.
            Return the corrected text only.
            """,
            name: "Reviewer Agent",
            description: "Quality Reviewer");
    }

    public Agents.ChatClientAgent CreateSummaryAgent()
    {
        return new Agents.ChatClientAgent(
            _chatClient,
            instructions:
            """
            Summarize the given text into a few concise sentences.
            """,
            name: "Summary Agent",
            description: "Summary Generator");
    }
}
