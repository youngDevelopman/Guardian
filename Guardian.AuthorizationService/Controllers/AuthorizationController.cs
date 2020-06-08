using Guardian.AuthorizationService;
using Guardian.AuthorizationService.Models;
using Guardian.Models.AuthorizationService;
using Guardian.Services.AuthorizationService;
using Guardian.Shared.Models;
using Guardian.Shared.Models.AuthorizationService;
using Guardian.Shared.Models.AuthorizationService.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Guardian.AuthService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorizationController : ControllerBase
    {
        private readonly ILogger<AuthorizationController> _logger;
        private readonly IUserService _userService;
        private readonly TokenManager _tokenManager;

        public AuthorizationController(ILogger<AuthorizationController> logger, IUserService userService, TokenManager tokenManager)
        {
            _logger = logger;
            _userService = userService;
            _tokenManager = tokenManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLogin user)
        {
            User authenticatedUser = await _userService.AuthenticateAsync(user.UserPoolId, user.Username, user.Password);
            if (authenticatedUser == null)
            {
                return Unauthorized("Password, username or User pool Id is incorrect");
            }
            
            OAuthTokenModel authTokenResponse = _tokenManager.GenerateToken(
                authenticatedUser.UserId.ToString());
            
            return Ok(authTokenResponse);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegister user)
        {
            string password = user.Password;
            var userToAdd = new User()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.Username
            };

            await _userService.CreateUserAsync(userToAdd, password);
            return Ok($"User {userToAdd.Username} has been added");
        }

        [HttpPost("validate")]
        public async Task<IActionResult> ValidateToken(TokenValidationRequest request)
        {
            bool isTokenValid = _tokenManager.ValidateToken(request.AccessToken, out Guid userId);

            bool isBelongsToPool = await _userService.IsUserBelongsToPoolAsync(userId, request.UserPoolId);

            if (!isTokenValid || !isBelongsToPool)
            {
                return Unauthorized("Failed to validate token.");
            }

            return Ok("Token is valid");
        }
    }
}
