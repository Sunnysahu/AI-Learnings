namespace AI_Translation_Agent.Models
{
    public sealed class TranslationResult
    {
        public string OriginalText { get; set; } = string.Empty;

        public string HindiTranslation { get; set; } = string.Empty;

        public string SpanishTranslation { get; set; } = string.Empty;

        public string Review { get; set; } = string.Empty;

        public string Summary { get; set; } = string.Empty;

        public bool PassedReview { get; set; }
    }
}
