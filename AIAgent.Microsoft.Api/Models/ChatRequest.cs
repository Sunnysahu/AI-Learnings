namespace AIAgent.Microsoft.Api.Models;

public sealed record ChatRequest(Guid? SessionId, string Message = "");