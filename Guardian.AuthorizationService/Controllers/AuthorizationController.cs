using Guardian.AuthorizationService;
using Guardian.AuthorizationService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Guardian.AuthService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorizationController : ControllerBase
    {
        private readonly ILogger<AuthorizationController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly TokenManager _tokenManager;

        public AuthorizationController(ILogger<AuthorizationController> logger, IUserRepository userRepository, TokenManager tokenManager)
        {
            _logger = logger;
            _userRepository = userRepository;
            _tokenManager = tokenManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginModel user)
        {
            User authenticatedUser = _userRepository.Authenticate(user.Username, user.Password);
            if (authenticatedUser == null)
            {
                return Unauthorized("Password or username is incorrect");
            }
            
            OAuthTokenModel authTokenResponse = _tokenManager.GenerateToken(authenticatedUser.UserId.ToString());
            
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

        [HttpPost("validate")]
        public async Task<IActionResult> ValidateToken(OAuthTokenModel user)
        {
            bool isValid = _tokenManager.ValidateToken(user.AccessToken);

            if (!isValid)
            {
                return Unauthorized(isValid);
            }

            return Ok(isValid);
        }
    }
}
