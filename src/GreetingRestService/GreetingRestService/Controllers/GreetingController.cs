using Microsoft.AspNetCore.Mvc;

namespace GreetingRestService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GreetingController : ControllerBase
    {
        [HttpGet]
        [Route("hello")]
        public IActionResult SayHello([FromQuery] string name)
        {
            return Ok($"Hello, {name}");
        }
    }
}
