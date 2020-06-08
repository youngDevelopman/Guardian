
using System.Threading.Tasks;
using Guardian.ResourceService.Services;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Guardian.ResourceService.Models;
using System;
using Microsoft.AspNetCore.Http;
using Guardian.Shared.Models;

namespace Guardian.ResourceService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResourcesController : ControllerBase
    {
        private readonly IProxyService _proxyService;
        public ResourcesController(IProxyService proxyService)
        {
            _proxyService = proxyService;
        }

        [HttpPost]
        public async Task<IActionResult> GetResource(GetResourceRequest request)
        {
            var proxy = await _proxyService.GenerateProxy(request.Domain, request.RelativePath);

            if(proxy == null)
            {
                return BadRequest("Resource not found.");
            }

            return Ok(proxy);
        }
    }
}