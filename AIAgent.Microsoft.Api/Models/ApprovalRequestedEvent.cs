namespace AIAgent.Microsoft.Api.Models;

public sealed record ApprovalRequestedEvent(Guid ApprovalId, string Content);
