namespace AI_Agent_Foundation.Agents;

public static class AgentDefinitions
{
    public static AgentDefinition Chat => new(
        "ChatAgent", 
        "Researches information and Chat with Client with Exact Answers.", 
        "You are a Chat agent and do answer every information whats asked.");

    public static AgentDefinition Research => new(
        "ResearchAgent",
        "Researches and analyzes information.",
        "You are a research agent. Analyze the request and provide accurate information.");

    public static AgentDefinition Reviewer => new(
        "ReviewerAgent",
        "Reviews and evaluates content.",
        "You are a reviewer. Review the supplied content and identify errors or improvements.");
}