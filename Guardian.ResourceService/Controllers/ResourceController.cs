
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
        private readonly IResourceService _resourceService;
        public ResourcesController(IResourceService resourceService)
        {
            _resourceService = resourceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetGateways()
        {
            var resources = await _resourceService.GetGateways();

            return Ok(resources);
        }

        [HttpPost]
        public async Task<IActionResult> GetResource(GetResourceRequest request)
        {
            var proxy = await _resourceService.GetResource(request);

            if(proxy == null)
            {
                return BadRequest("Resource not found.");
            }

            return Ok(proxy);
        }
    }
}