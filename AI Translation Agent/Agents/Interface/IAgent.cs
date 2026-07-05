namespace AI_Translation_Agent.Agents.Interface;

public interface IAgent
{
    string Name { get; }

    string Description { get; }

    Task<string> ExecuteAsync(string input);
}