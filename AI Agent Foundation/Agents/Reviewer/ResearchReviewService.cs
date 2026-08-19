using AI_Agent_Foundation.Agents.Research;

namespace AI_Agent_Foundation.Agents.Reviewer;

public sealed class ResearchReviewService(ResearchAgent researchAgent, ReviewerAgent reviewerAgent)
{
    public async Task<string> RunAsync(string request, CancellationToken cancellationToken = default)
    {
        var research = await researchAgent.ResearchAsync(request, cancellationToken);

        var review = await reviewerAgent.ReviewAsync(research, cancellationToken);

        return review;
    }
}