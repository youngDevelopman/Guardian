using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Guardian.AuthorizationService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorizationController : ControllerBase
    {
        public AuthorizationController()
        {
            Console.WriteLine();
        }
        [HttpPost]
        public async Task<IActionResult> Login()
        {
            return Ok("LOGIN");
        }

        [HttpPost]
        public async Task<IActionResult> Register()
        {
            return Ok("REGISTER");
        }
        [HttpGet]
        public async Task<IActionResult> Test()
        {
            return Ok("GET 111");
        }
    }
}