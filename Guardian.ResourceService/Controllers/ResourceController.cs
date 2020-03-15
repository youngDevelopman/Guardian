
using System.Threading.Tasks;
using Guardian.ResourceService.Services;
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

        public async Task<IActionResult> Index()
        {
            var resources = await _resourceService.GetResources();
            return Ok(resources);
        }
    }
}