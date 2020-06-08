using Guardian.ResourceService.Models;
using Guardian.Shared.Models;
using Guardian.Shared.Models.ResourceService;
using Guardian.Shared.Models.ResourceService.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guardian.ResourceService.Services
{
    public interface IResourceService
    {
        /// <summary>
        /// Get all gateways
        /// </summary>
        /// <returns></returns>
        Task<List<GatewayShortInfo>> GetGateways();

        /// <summary>
        /// Searches for gateway bu its id.
        /// </summary>
        /// <param name="gatewayId"></param>
        /// <returns></returns>
        Task<Resource> GetGateway(string gatewayId);

        /// <summary>
        /// Adds resource
        /// </summary>
        /// <param name="resource">Object to add</param>
        /// <returns></returns>
        Task AddGateway(Resource resource);

        /// <summary>
        /// Updates gateway. Id, Creation Date and Segments will not be updated.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> UpdateGateway(Resource request);

        /// <summary>
        /// Updates gateway segment.
        /// </summary>
        /// <param name="gatewayId">Gateway Id.</param>
        /// <param name="segment">Resource segment.</param>
        /// <returns>Updated Resource</returns>
        Task<Resource> UpdateSegment(string gatewayId, ResourceSegment segment);

        /// <summary>
        /// Adds segments to the gateway
        /// </summary>
        /// <param name="gatewayId">>Gateway Id.</param>
        /// <param name="segments">Resource segment.</param>
        /// <returns>>Updated Resource</returns>
        Task<bool> AddGatewaySegments(string gatewayId, List<ResourceSegment> segments);

        /// <summary>
        /// Adds root segment to the gateway.
        /// </summary>
        /// <param name="gatewayId">>Gateway Id.</param>
        /// <param name="segments">Resource segment.</param>
        /// <returns>>Updated Resource</returns>
        Task<Resource> AddRootSegment(string gatewayId, ResourceSegment request);


        /// <summary>
        /// Adds child segment to the gateway.
        /// </summary>
        /// <param name="gatewayId">>Gateway Id.</param>
        /// <param name="parentSegmentId">Parent segment id.</param>
        /// <param name="segment">Resource segment to add.</param>
        /// <returns>>Updated Resource</returns>
        Task<Resource> AddChildSegment(string gatewayId, string parentSegmentId, ResourceSegment segment);


        /// <summary>
        /// Deletes from the gateway.
        /// </summary>
        /// <param name="gatewayId">>Gateway Id.</param>
        /// <param name="segmentId">Segment Id.</param>
        /// <returns>>Updated Resource</returns>
        Task<Resource> DeleteSegment(string gatewayId, string segmentId);

        /// <summary>
        /// Deletes gateway.
        /// </summary>
        /// <param name="gatewayId"></param>
        /// <returns></returns>
        Task<bool> DeleteGateway(string gatewayId);
    }
}
