using AI_Translation_Agent.Models;
using AI_Translation_Agent.Services;

namespace AI_Translation_Agent.Agents;

public class AIAgent
{
    private readonly IAIChatService _chatService;
    private readonly AgentOptions _options;

    public AIAgent(IAIChatService chatService, AgentOptions options)
    {
        _chatService = chatService;
        _options = options;
    }

    public string Name => _options.Name;

    public string Description => _options.Description;

    public async Task ExecuteAsync(WorkflowContext context)
    {
        string input = _options.Action switch
        {
            AgentAction.HindiTranslation => context.OriginalText,

            AgentAction.SpanishTranslation => context.OriginalText,

            AgentAction.Review =>
            $$"""
            Original: {{context.OriginalText}}

            Hindi: {{context.HindiTranslation}}

            Spanish: {{context.SpanishTranslation}}
            """,

            AgentAction.Summary => context.Review,

            _ => throw new InvalidOperationException()
        };

        string prompt = 
            $"""
            {_options.Instructions}

            Input: {input}
            """;

        string response = await _chatService.AskAsync(prompt);

        switch (_options.Action)
        {
            case AgentAction.HindiTranslation:

                context.HindiTranslation = response;

                break;

            case AgentAction.SpanishTranslation:

                context.SpanishTranslation = response;

                break;

            case AgentAction.Review:

                context.Review = response;

                context.PassedReview = response.StartsWith("PASS", StringComparison.OrdinalIgnoreCase);

                break;

            case AgentAction.Summary:

                context.Summary = response;

                break;
        }
    }
}