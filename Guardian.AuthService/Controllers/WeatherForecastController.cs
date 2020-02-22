using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Guardian.AuthService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorizationController : ControllerBase
    {
        private readonly ILogger<AuthorizationController> _logger;

        public AuthorizationController(ILogger<AuthorizationController> logger)
        {
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Get()
        {
            return Ok("LOGIN");
        }
    }
}
