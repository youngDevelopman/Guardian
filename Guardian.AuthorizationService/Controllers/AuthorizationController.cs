using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Guardian.AuthorizationService;
using Guardian.AuthorizationService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Guardian.AuthService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorizationController : ControllerBase
    {
        private readonly ILogger<AuthorizationController> _logger;
        private readonly IUserRepository _userRepository;

        public AuthorizationController(ILogger<AuthorizationController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginModel user)
        {
            User authenticatedUser = _userRepository.Authenticate(user.Username, user.Password);
            if (authenticatedUser == null)
            {
                return Unauthorized("Password or username is incorrect");
            }
            
            var tokenManager = new TokenManager();
            OAuthTokenResponse authTokenResponse = tokenManager.GenerateToken(authenticatedUser.UserId.ToString());
            
            return Ok(authTokenResponse);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterModel user)
        {
            string password = user.Password;
            var userToAdd = new User()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.Username
            };

            _userRepository.CreateUser(userToAdd, password);
            return Ok($"User {userToAdd.Username} has been added");
        }
    }
}
