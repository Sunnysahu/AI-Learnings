using AIAgent.Microsoft.Api.Models;
using AIAgent.Microsoft.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace AIAgent.Microsoft.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class TranslationController : ControllerBase
    {
        private readonly WorkflowService _workflowService;

        public TranslationController(WorkflowService workflowService) => _workflowService = workflowService;

        [HttpPost]
        public async Task<IActionResult> Translate([FromBody] TranslationRequest request)
        {
            string result = await _workflowService.ExecuteAsync(request.Text);

            TranslationResponse response = new()
            {
                Original = request.Text,
                Summary = result
            };

            return Ok(response);
        }
    }

}
