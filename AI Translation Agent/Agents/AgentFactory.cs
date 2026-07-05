namespace AI_Translation_Agent.Agents;

public interface IAIAgentFactory
{
    AIAgent CreateHindiTranslator();

    AIAgent CreateSpanishTranslator();

    AIAgent CreateReviewAgent();

    AIAgent CreateSummaryAgent();
}
