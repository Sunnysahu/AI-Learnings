using AI_Translation_Agent.Models;
using AI_Translation_Agent.Orchestrators;
using Microsoft.AspNetCore.Mvc;

namespace AI_Translation_Agent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AIController : ControllerBase
    {
        private readonly IAgentOrchestrator _orchestrator;

        public AIController(IAgentOrchestrator orchestrator) => _orchestrator = orchestrator;

        [HttpPost("translate")]
        public async Task<IActionResult> Translate([FromBody] TranslationRequest request)
        {
            TranslationResult result = await _orchestrator.ExecuteAsync(request.Text);

            return Ok(result);
        }
    }
}
