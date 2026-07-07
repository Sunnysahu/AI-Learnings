using AIAgent.Microsoft.Api.Models;

namespace AIAgent.Microsoft.Api.Services;

public interface IAgentService
{
    Task<TranslationResponse> TranslateAsync(string text);
}