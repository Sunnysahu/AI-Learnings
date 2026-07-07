using AIAgent.Microsoft.Api.Models;

namespace AIAgent.Microsoft.Api.Services;

public interface IChatAgentService
{
    Task<ChatResponse> AskAsync(string message);
}