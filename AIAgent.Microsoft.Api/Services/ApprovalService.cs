using System.Collections.Concurrent;

namespace AIAgent.Microsoft.Api.Services;

public sealed class ApprovalService
{
    private readonly ConcurrentDictionary<Guid, bool> _approvals = new();

    public void Approve(Guid approvalId) => _approvals[approvalId] = true;

    public void Reject(Guid approvalId) => _approvals[approvalId] = false;

    public bool? GetDecision(Guid approvalId) => _approvals.TryGetValue(approvalId, out bool decision) ? decision : null;
}