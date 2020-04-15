
using System.Threading.Tasks;
using Guardian.ResourceService.Services;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Guardian.ResourceService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResourceController : ControllerBase
    {
        private readonly IResourceService _resourceService;
        public ResourceController(IResourceService resourceService)
        {
            _resourceService = resourceService;
        }

        [HttpGet("{url}")]
        public async Task<IActionResult> GetResources(string url)
        {
            var resources = await _resourceService.GetResources();

            // TODO: Change 
            return Ok(resources.First());
        }
    }
}