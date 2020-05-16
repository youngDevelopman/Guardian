using Guardian.ResourceService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Guardian.ResourceService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GatewaysController : Controller
    {
        private readonly IResourceService _resourceService;
        public GatewaysController(IResourceService resourceService)
        {
            _resourceService = resourceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetGateways()
        {
            var resources = await _resourceService.GetGateways();

            return Ok(resources);
        }
    }
}