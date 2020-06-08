using Guardian.ResourceService.Models;
using Guardian.Shared.Models;
using Guardian.Shared.Models.ResourceService.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guardian.ResourceService.Services
{
    public interface IResourceService
    {
        
        Task<GetResourceResponse> GetResource(GetResourceRequest request);

        Task<GetGatewaysResponse> GetGateways();

        Task<GetGatewayResponse> GetGateway(string gatewayId);

        /// <summary>
        /// Adds resource
        /// </summary>
        /// <param name="resource">Object to add</param>
        /// <returns></returns>
        Task AddGateway(Resource resource);

        Task<bool> UpdateGateway(Resource request);

        Task<Resource> UpdateSegment(string gatewayId, ResourceSegment segment);

        Task<bool> AddGatewaySegments(string gatewayId, List<ResourceSegment> segments);

        Task<Resource> AddRootSegment(string gatewayId, ResourceSegment request);

        Task<Resource> AddChildSegment(string gatewayId, string parentSegmentId, ResourceSegment request);

        Task<Resource> DeleteSegment(string gatewayId, string segmentId);

        Task<bool> DeleteGateway(string gatewayId);
    }
}
