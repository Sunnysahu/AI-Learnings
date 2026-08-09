using AIAgent.Microsoft.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AIAgent.Microsoft.Api.Data;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

    public DbSet<ChatHistory> ChatHistory => Set<ChatHistory>();
}
