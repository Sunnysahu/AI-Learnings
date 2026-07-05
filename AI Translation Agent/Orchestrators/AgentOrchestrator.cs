using AI_Translation_Agent.Agents;
using AI_Translation_Agent.Models;
using AI_Translation_Agent.Workflows;

namespace AI_Translation_Agent.Orchestrators;

public class AgentOrchestrator : IAgentOrchestrator
{
    private readonly IAIAgentFactory _factory;

    public AgentOrchestrator(IAIAgentFactory factory) => _factory = factory;


    public async Task<TranslationResult> ExecuteAsync(string input)
    {
        var workflow = new SequentialAgentWorkflow()
            .Add(_factory.CreateHindiTranslator())
            .Add(_factory.CreateSpanishTranslator())
            .Add(_factory.CreateReviewAgent())
            .Add(_factory.CreateSummaryAgent());

        return await workflow.RunAsync(input);
    }
}