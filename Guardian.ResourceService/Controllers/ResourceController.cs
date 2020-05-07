
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
        
        [HttpPost("domain-user-pool")]
        public async Task<IActionResult> GetUserPool(DomainUserPool request)
        {
            var response = await _resourceService.GetUserPoolFromDomain(request.Domain);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> FindResource(ResourceServiceRequest request)
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