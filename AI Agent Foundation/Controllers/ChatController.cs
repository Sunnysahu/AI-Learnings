using AI_Agent_Foundation.Agents.Chat;
using AI_Agent_Foundation.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AI_Agent_Foundation.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class ChatController(ChatAgent chatAgent) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<string>> Chat(ChatRequest request, CancellationToken cancellationToken)
    {
        var response = await chatAgent.ChatAsync(request.Message, cancellationToken);

        return Ok(response);
    }
}