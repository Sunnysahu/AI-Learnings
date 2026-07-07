using AIAgent.Microsoft.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace AIAgent.Microsoft.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService) => _chatService = chatService;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string response = await _chatService.AskAsync("Hello! My name is Sunny Sahu.");

            return Ok(response);
        }
    }
}
