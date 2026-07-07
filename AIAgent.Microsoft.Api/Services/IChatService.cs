namespace AIAgent.Microsoft.Api.Services;

public interface IChatService
{
    Task<string> AskAsync(string message);
}