
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
        public async Task<IActionResult> FindResource(ResourceServiceRequest request)
        {
            var resource = await _resourceService.GetResource(request);

            if(resource == null)
            {
                return BadRequest("Resource not found.");
            }

            var resourceServiceResponse = new ResourceServiceResponse()
            {
                UserPoolId = Guid.NewGuid(),
                IsAuthenticationRequired = resource.Destination.RequiresAuthentication,
                IsProxyDefined = true,
                SourceUrl = request.BasePath + request.Path,
                ProxyUrl = resource.Destination.Uri,
            };

            return Ok(resourceServiceResponse);
        }
    }
}