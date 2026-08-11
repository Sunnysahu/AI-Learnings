namespace AIAgent.Microsoft.Api.Models;

public sealed record ApprovalRequestedMessage(Guid ApprovalId, string Content);