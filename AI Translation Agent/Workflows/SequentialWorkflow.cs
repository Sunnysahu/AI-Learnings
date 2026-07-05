using AI_Translation_Agent.Agents;
using AI_Translation_Agent.Models;

namespace AI_Translation_Agent.Workflows;

public class SequentialAgentWorkflow : IAgentWorkflow
{
    private readonly List<AIAgent> _agents = [];

    public SequentialAgentWorkflow Add(AIAgent agent)
    {
        _agents.Add(agent);
        return this;
    }

    public async Task<TranslationResult> RunAsync(string input)
    {
        WorkflowContext context = new()
        {
            OriginalText = input
        };

        foreach (var agent in _agents)
        {
            await agent.ExecuteAsync(context);
        }

        return new TranslationResult
        {
            OriginalText = context.OriginalText,
            HindiTranslation = context.HindiTranslation,
            SpanishTranslation = context.SpanishTranslation,
            Review = context.Review,
            Summary = context.Summary,
            PassedReview = context.PassedReview
        };
    }
}