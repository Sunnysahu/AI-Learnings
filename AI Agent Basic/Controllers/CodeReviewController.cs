using AI_Agent_Basic.Agents;
using AI_Agent_Basic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AI_Agent_Basic.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CodeReviewController : ControllerBase
{
    private readonly AgentFactory _agentFactory;

    public CodeReviewController(AgentFactory agentFactory) =>  _agentFactory = agentFactory;
        

    [HttpPost]
    public async Task<IActionResult> Review([FromBody] AgentRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Message))
        {
            return BadRequest("Message is required.");
        }

        var agent = _agentFactory.Create(AgentDefinitions.CodeReviewer);

        var response = await agent.RunAsync(request.Message, cancellationToken: cancellationToken);

        return Ok(new
        {
            response = response.Text
        });
    }
}

