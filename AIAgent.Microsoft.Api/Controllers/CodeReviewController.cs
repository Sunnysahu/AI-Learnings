using AIAgent.Microsoft.Api.Models;
using AIAgent.Microsoft.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AIAgent.Microsoft.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodeReviewController : ControllerBase
    {
        private readonly WorkflowService _workflow;

        public CodeReviewController(WorkflowService workflow) => _workflow = workflow;

        [HttpPost]
        public async Task<IActionResult> Review([FromBody] CodeReviewRequest request)
        {
            string result = await _workflow.ExecuteCodeReviewAsync(request.Code);

            return Ok(new CodeReviewResponse(result));
        }
    }
}
