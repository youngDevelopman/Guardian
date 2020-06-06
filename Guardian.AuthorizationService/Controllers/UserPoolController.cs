using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Guardian.Services.AuthorizationService;
using Guardian.Shared.Models;
using Guardian.Shared.Models.AuthorizationService;
using Guardian.Shared.Models.AuthorizationService.Response;
using Microsoft.AspNetCore.Mvc;

namespace Guardian.AuthorizationService.Controllers
{
    [ApiController]
    [Route("user-pool")]
    public class UserPoolController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserPoolController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserPools()
        {
            var userPools = await _userService.GetUserPoolsAsync();
            var response = new GetUserPoolsResponse()
            {
                UserPools = userPools.Select(x => new UserPool() { Name = x.Name, UserPoolId = x.UserPoolId, CreationDate = x.CreationDate })
            };

            return Ok(response);
        }
    }
}