using Microsoft.AspNetCore.Mvc;

namespace rescomac_back.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [NonAction]
        public async Task<IActionResult> Post()
        {
            throw new NotImplementedException();
        }
    }
}