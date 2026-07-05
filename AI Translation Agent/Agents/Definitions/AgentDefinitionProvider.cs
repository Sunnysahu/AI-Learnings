namespace AI_Translation_Agent.Agents.Definitions;

public class AgentDefinitionProvider : IAgentDefinitionProvider
{
    public AgentDefinition Hindi() => new()
    {
        Name = "Hindi Translator",
        Description = "Translate English to Hindi.",
        Instructions =
        """
        Translate the given English text into Hindi.

        Rules:
        - Translate only.
        - Do not explain.
        - Keep names unchanged.
        """,
        Action = AgentAction.HindiTranslation
    };

    public AgentDefinition Spanish() => new()
    {
        Name = "Spanish Translator",
        Description = "Translate English to Spanish.",
        Instructions =
        """
        Translate the given English text into Spanish.

        Rules:
        - Translate only.
        - Do not explain.
        - Keep names unchanged.
        """,
        Action = AgentAction.SpanishTranslation
    };

    public AgentDefinition Reviewer() => new()
    {
        Name = "Review Agent",

        Description = "Reviews translations.",

        Instructions =
        """
        You are a Senior Translation QA Reviewer.

        You are NOT a translator.

        Review both translations.

        Verify:

        - Grammar
        - Accuracy
        - Meaning
        - Natural language
        - Proper nouns remain unchanged

        If both translations are correct respond ONLY with:

        PASS

        Otherwise respond in this format:

        FAIL

        Hindi Issues:
        ...

        Spanish Issues:
        ...

        Suggestions:
        ...
        """,
        Action = AgentAction.Review
    };

    public AgentDefinition Summary() => new()
    {
        Name = "Summary Agent",

        Description = "Summarizes QA results.",

        Instructions =
        """
        You are an executive assistant.

        Read the QA review.

        If the review says PASS then respond with:

        The translations passed quality review.

        Otherwise summarize the problems in a few sentences.
        """,
        Action = AgentAction.Summary
    };
}