using AIAgent.Microsoft.Api.Models;
using AIAgent.Microsoft.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AIAgent.Microsoft.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        private readonly IAgentService _service;

        public AgentController(IAgentService service) => _service = service;


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            TranslationResponse response = await _service.TranslateAsync("Hi, I am Sunny Sahu from Jamshedpur, Jharkhand.");

            //var response = await _service.TranslateAsync("What is 456 * 87?");

            return Ok(response);
        }
    }
}
