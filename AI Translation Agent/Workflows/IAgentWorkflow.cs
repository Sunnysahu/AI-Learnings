using AI_Translation_Agent.Models;

namespace AI_Translation_Agent.Workflows;

public interface IAgentWorkflow
{
    Task<TranslationResult> RunAsync(string input);
}