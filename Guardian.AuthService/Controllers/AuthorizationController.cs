using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Guardian.AuthorizationService;
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
        public async Task<IActionResult> Login(User user)
        {
            User u = new UserRepository().GetUser(user.Username);
            if (u == null)
            {
                return Unauthorized();
            }
            bool credentials = u.Password.Equals(user.Password);
            if (!credentials)
            {
                return Unauthorized();
            }
            var tokenManager = new TokenManager();
            var token = tokenManager.GenerateToken(user.Username);
            return Ok(token);
        }
    }
}
