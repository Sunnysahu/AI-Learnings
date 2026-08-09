using AIAgent.Microsoft.Api.Data;
using AIAgent.Microsoft.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AIAgent.Microsoft.Api.Repositories;

public sealed class ChatHistoryRepository(ApplicationDbContext db ) : IChatHistoryRepository
{
    private readonly ApplicationDbContext _db = db;

    public async Task AddAsync(ChatHistory history)
    {
        _db.ChatHistory.Add(history);

        await _db.SaveChangesAsync();
    }

    public async Task<List<ChatHistory>> GetBySessionAsync(Guid sessionId)
    {
        return await _db.ChatHistory
            .Where(x => x.SessionId == sessionId)
            .OrderBy(x => x.CreatedAt)
            .ToListAsync();
    }
}