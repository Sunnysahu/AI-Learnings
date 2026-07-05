using AI_Translation_Agent.Agents.Definitions;
using AI_Translation_Agent.Services;

namespace AI_Translation_Agent.Agents;

public class AIAgentFactory : IAIAgentFactory
{
    private readonly IAIChatService _chatService;
    private readonly IAgentDefinitionProvider _definitions;

    public AIAgentFactory(IAIChatService chatService, IAgentDefinitionProvider definitions)
    {
        _chatService = chatService;
        _definitions = definitions;
    }

    public AIAgent CreateHindiTranslator()
    {
        var d = _definitions.Hindi();

        return new AIAgent(_chatService, new AgentOptions
        {
            Name = d.Name,
            Description = d.Description,
            Instructions = d.Instructions,
            Action = d.Action
        });
    }

    public AIAgent CreateSpanishTranslator()
    {
        var d = _definitions.Spanish();

        return new AIAgent(_chatService, new AgentOptions
        {
            Name = d.Name,
            Description = d.Description,
            Instructions = d.Instructions,
            Action = d.Action
        });
    }

    public AIAgent CreateReviewAgent()
    {
        var d = _definitions.Reviewer();

        return new AIAgent(_chatService, new AgentOptions
        {
            Name = d.Name,
            Description = d.Description,
            Instructions = d.Instructions,
            Action = d.Action
        });
    }

    public AIAgent CreateSummaryAgent()
    {
        var d = _definitions.Summary();

        return new AIAgent(_chatService, new AgentOptions
        {
            Name = d.Name,
            Description = d.Description,
            Instructions = d.Instructions,
            Action = d.Action
        });
    }
}