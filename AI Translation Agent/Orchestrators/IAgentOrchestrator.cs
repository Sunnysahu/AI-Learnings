using AI_Translation_Agent.Models;

namespace AI_Translation_Agent.Orchestrators;

public interface IAgentOrchestrator
{
    Task<TranslationResult> ExecuteAsync(string input);
}