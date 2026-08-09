using AIAgent.Microsoft.Api.Models;

namespace AIAgent.Microsoft.Api.Repositories;

public interface IChatHistoryRepository
{
    Task AddAsync(ChatHistory history);

    Task<List<ChatHistory>> GetBySessionAsync(Guid sessionId);
}