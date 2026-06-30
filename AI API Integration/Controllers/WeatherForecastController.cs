using Microsoft.AspNetCore.Mvc;

namespace AI_API_Integration.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public WeatherForecastController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var secret = _configuration["JWT_SECRET"];

            return Ok(new
            {
                Server = _configuration["DB_SERVER"],
                Database = _configuration["DB_NAME"],
                Email = _configuration["ADMIN_EMAIL"],
                Secret = _configuration["JWT_SECRET"]
            });
        }
    }
}
