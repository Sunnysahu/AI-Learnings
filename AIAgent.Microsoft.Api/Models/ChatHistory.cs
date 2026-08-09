using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIAgent.Microsoft.Api.Models;

[Table("ChatHistory")]
public class ChatHistory
{
    [Key]
    public int Id { get; set; }

    public Guid SessionId { get; set; }

    public string Role { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}