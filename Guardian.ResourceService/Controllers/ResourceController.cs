
using System.Threading.Tasks;
using Guardian.ResourceService.Services;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Guardian.ResourceService.Models;
using System;
using Microsoft.AspNetCore.Http;

namespace Guardian.ResourceService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResourcesController : ControllerBase
    {
        private readonly IResourceService _resourceService;
        public ResourcesController(IResourceService resourceService)
        {
            _resourceService = resourceService;
        }

        [HttpPost]
        public async Task<IActionResult> GenerateProxy(ResourceServiceRequest request)
        {
            var proxy = await _resourceService.GenerateProxy(request);

            if(proxy == null)
            {
                return BadRequest("Resource not found.");
            }

            return Ok(proxy);
        }
    }
}