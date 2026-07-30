using AIAgent.Microsoft.Api.Models;
using AIAgent.Microsoft.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace AIAgent.Microsoft.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class ChatController : ControllerBase
    {
        private readonly WorkflowService _workflow;

        public ChatController(WorkflowService workflow) => _workflow = workflow;

        [HttpPost]
        public async Task<IActionResult> Chat(ChatRequest request)
        {
            ChatResponse result = await _workflow.ExecuteChatAsync(request.SessionId, request.Message);

            return Ok(result);
        }
    }
}
