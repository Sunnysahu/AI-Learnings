using AIAgent.Microsoft.Api.Models;
using Agents = Microsoft.Agents.AI;
using AI = Microsoft.Extensions.AI;

namespace AIAgent.Microsoft.Api.Services;

public sealed class AgentService : IAgentService
{
    private readonly IAgentFactory _factory;

    public AgentService(IAgentFactory factory) => _factory = factory;

    public async Task<TranslationResponse> TranslateAsync(string text)
    {
        TranslationResponse result = new();

        result.Original = text;

        // Hindi
        var hindiAgent = _factory.CreateHindiAgent();
        var hindiSession = await hindiAgent.CreateSessionAsync();

        var hindiResponse = await hindiAgent.RunAsync(text, hindiSession);

        result.Hindi = hindiResponse.Text;

        // Spanish
        var spanishAgent = _factory.CreateSpanishAgent();
        var spanishSession = await spanishAgent.CreateSessionAsync();

        var spanishResponse = await spanishAgent.RunAsync(text, spanishSession);

        result.Spanish = spanishResponse.Text;

        // Reviewer
        var reviewerAgent = _factory.CreateReviewerAgent();
        var reviewerSession = await reviewerAgent.CreateSessionAsync();

        string reviewInput =
        $"""
            Hindi: {result.Hindi}

            Spanish: {result.Spanish}
        """;

        var reviewResponse = await reviewerAgent.RunAsync(reviewInput, reviewerSession);

        result.Reviewed = reviewResponse.Text;

        // Summary
        var summaryAgent = _factory.CreateSummaryAgent();
        var summarySession = await summaryAgent.CreateSessionAsync();

        var summaryResponse = await summaryAgent.RunAsync(result.Reviewed, summarySession);

        result.Summary = summaryResponse.Text;

        return result;
    }
}