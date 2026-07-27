using AIAgent.Microsoft.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace AIAgent.Microsoft.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkflowController : ControllerBase
    {
        private readonly WorkflowService _workflowService;

        public WorkflowController(WorkflowService workflowService) => _workflowService = workflowService;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string result = await _workflowService.ExecuteAsync("Hello, my name is Sunny.");

            return Ok(result);
        }
    }
}
