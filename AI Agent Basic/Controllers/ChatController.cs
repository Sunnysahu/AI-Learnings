using AI_Agent_Basic.Agents;
using AI_Agent_Basic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AI_Agent_Basic.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChatController : ControllerBase
{

    private readonly AgentFactory _agentFactory;

    public ChatController(AgentFactory agentFactory) => _agentFactory = agentFactory;
    
    [HttpPost]
    public async Task<IActionResult> Chat([FromBody] AgentRequest request, CancellationToken cancellationToken)
    {

        if (string.IsNullOrWhiteSpace(request.Message))
        {
            return BadRequest(new
            {
                Error = "Prompt is Required"
            });
        }

        var agent = _agentFactory.Create(AgentDefinitions.Chat);

        var response = await agent.RunAsync(request.Message, cancellationToken: cancellationToken);

        return Ok(new
        {
            response = response.Text
        });
    }
}

