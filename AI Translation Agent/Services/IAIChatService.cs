namespace AI_Translation_Agent.Services;

public interface IAIChatService
{
    Task<string> AskAsync(string prompt);
}

