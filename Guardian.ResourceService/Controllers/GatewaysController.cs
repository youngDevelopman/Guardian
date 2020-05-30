using Guardian.ResourceService.Services;
using Guardian.Shared.Models;
using Microsoft.AspNetCore.Http;
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

        [HttpGet("{gatewayId}")]
        public async Task<IActionResult> GetGateway(string gatewayId)
        {
            var gateway = await _resourceService.GetGateway(gatewayId);

            return Ok(gateway);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateGateway(UpdateGatewayRequest updateRequest)
        {
            var isModified = await _resourceService.UpdateGateway(updateRequest);

            if (!isModified)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok();
        }
        
        [HttpPost("segment/root")]
        public async Task<IActionResult> AddRootSegment(AddRootSegmentRequest addRootSegmentRequest)
        {
            var result = await _resourceService.AddRootSegment(addRootSegmentRequest);
            return Ok(result);
        }
    }
}