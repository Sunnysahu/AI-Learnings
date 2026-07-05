namespace AI_Translation_Agent.Models;

public sealed class WorkflowContext
{
    public string OriginalText { get; set; } = "";

    public string HindiTranslation { get; set; } = "";

    public string SpanishTranslation { get; set; } = "";

    public string Review { get; set; } = "";

    public string Summary { get; set; } = "";

    public bool PassedReview { get; set; }
}