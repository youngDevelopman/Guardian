using AutoMapper;
using Guardian.ResourceService.Models;
using Guardian.ResourceService.Services;
using Guardian.Shared.Models;
using Guardian.Shared.Models.ResourceService.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Threading.Tasks;

namespace Guardian.ResourceService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GatewaysController : Controller
    {
        private readonly IResourceService _resourceService;
        private readonly IMapper _mapper;

        public GatewaysController(IResourceService resourceService, IMapper mapper)
        {
            _resourceService = resourceService;
            _mapper = mapper;
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
        public async Task<IActionResult> AddGateway(AddGatewayRequest request)
        {
            var resourceToAdd = _mapper.Map<Resource>(request.GatewayToAdd);

            await _resourceService.AddGateway(resourceToAdd);

            return Ok();
        }

        [HttpPut("{gatewayId}")]
        public async Task<IActionResult> UpdateGateway(string gatewayId, UpdateGatewayRequest updateRequest)
        {
            var resourceToUpdate = _mapper.Map<Resource>(updateRequest.GatewayToUpdate);
            resourceToUpdate.Id = gatewayId;

            var isModified = await _resourceService.UpdateGateway(resourceToUpdate);

            if (!isModified)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok();
        }

        [HttpDelete("{gatewayId}")]
        public async Task<IActionResult> DeleteGateway(string gatewayId)
        {
            var result = await _resourceService.DeleteGateway(gatewayId);
            return Ok(result);
        }
        
        [HttpPost("{gatewayId}/segments/root")]
        public async Task<IActionResult> AddRootSegment(string gatewayId, AddRootSegmentRequest addRootSegmentRequest)
        {
            var result = await _resourceService.AddRootSegment(gatewayId, addRootSegmentRequest);
            return Ok(result);
        }

        [HttpPost("{gatewayId}/segments/child")]
        public async Task<IActionResult> AddChildSegment(string gatewayId, AddChildSegmentRequest addChildSegmentRequest)
        {
            var result = await _resourceService.AddChildSegment(gatewayId, addChildSegmentRequest);
            return Ok(result);
        }

        [HttpDelete("{gatewayId}/segments/{segmentId}")]
        public async Task<IActionResult> DeleteSegment(string gatewayId, string segmentId)
        {
            var result = await _resourceService.DeleteSegment(gatewayId, segmentId);
            return Ok(result);
        }
    }
}