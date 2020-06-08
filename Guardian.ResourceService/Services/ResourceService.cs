using Guardian.ResourceService.Models;
using Guardian.Shared.Models;
using Guardian.Shared.Models.ResourceService.Request;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guardian.ResourceService.Services
{
    public class ResourceService : IResourceService
    {
        // Collection (table) that stores information about endpoints
        private readonly IMongoCollection<Resource> _resourceCollection;

        public ResourceService(IMongoDatabase mongoDatabase)
        {
            _resourceCollection = mongoDatabase.GetCollection<Resource>("resources");
        }

        /// <inheritdoc/>
        public async Task<List<GatewayShortInfo>> GetGateways()
        {
            var filter = Builders<Resource>.Filter.Empty;
            var projection = Builders<Resource>.Projection.Expression(p =>
                new GatewayShortInfo { Name = p.Name, CreationDate = p.CreationDate, Description = p.Description, GatewayId = p.Id });
            var options = new FindOptions<Resource, GatewayShortInfo>();
            options.Projection = projection;

            var cursor = await _resourceCollection.FindAsync(filter, options);

            var resourceList = await cursor.ToListAsync();

            return resourceList;
        }

        /// <inheritdoc/>
        public async  Task<GetGatewayResponse> GetGateway(string gatewayId)
        {
            var filter = Builders<Resource>.Filter.Eq(x => x.Id, gatewayId);
            var findResult =  await _resourceCollection.FindAsync(filter);

            var gateway = findResult.ToListAsync().Result.FirstOrDefault();

            if (gateway == null)
            {
                throw new Exception($"Gateway with {gatewayId} does not exist.");
            }

            var response = new GetGatewayResponse()
            {
                Gateway = gateway,
            };

            return response;
        }

        /// <inheritdoc/>
        public async Task AddGateway(Resource resource)
        {
            await _resourceCollection.InsertOneAsync(resource);
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateGateway(Resource resource)
        {
            var filter = Builders<Resource>.Filter.Eq(x => x.Id, resource.Id);

            var update = Builders<Resource>.Update
                .Set(x => x.UserPoolId, resource.UserPoolId)
                .Set(x => x.Name, resource.Name)
                .Set(x => x.Domain, resource.Domain)
                .Set(x => x.Description, resource.Description);

            var result = await _resourceCollection.UpdateOneAsync(filter, update);
            if(!result.IsAcknowledged)
            {
                return false;
            }

            return true;
        }

        /// <inheritdoc/>
        public async Task<bool> AddGatewaySegments(string gatewayId, List<ResourceSegment> segments)
        {
            var filter = Builders<Resource>.Filter.Eq(x => x.Id, gatewayId);

            var update = Builders<Resource>.Update
                .Set(x => x.Segments, segments);

            var result = await _resourceCollection.UpdateOneAsync(filter, update);
            if (!result.IsAcknowledged)
            {
                return false;
            }

            return true;
        }

        /// <inheritdoc/>
        public async Task<Resource> AddRootSegment(string gatewayId, ResourceSegment resource)
        {

            var filter = Builders<Resource>.Filter.Eq(x => x.Id, gatewayId);
            var update = Builders<Resource>.Update.Push<ResourceSegment>(x => x.Segments, resource);

            await _resourceCollection.FindOneAndUpdateAsync(filter, update);
            var result = await this.GetGateway(gatewayId);
            
            return result.Gateway;
        }

        /// <inheritdoc/>
        public async Task<Resource> AddChildSegment(string gatewayId, string parentSegmentId, ResourceSegment segment)
        {
            var response = await this.GetGateway(gatewayId);
            var gateway = response.Gateway;

            var segmentsToUpdate = this.AddSegmentToParent(parentSegmentId, segment, gateway.Segments);
            gateway.Segments = segmentsToUpdate;

            await this.AddGatewaySegments(gatewayId, segmentsToUpdate);

            return gateway;
        }

        /// <inheritdoc/>
        public async Task<Resource> UpdateSegment(string gatewayId, ResourceSegment segment)
        {
            var response = await this.GetGateway(gatewayId);
            var gateway = response.Gateway;

            var segmentsToUpdate = this.UpdateSegment(segment, gateway.Segments);
            await this.AddGatewaySegments(gatewayId, segmentsToUpdate);

            return gateway;
        }

        /// <inheritdoc/>
        public async  Task<bool> DeleteGateway(string gatewayId)
        {
            var filter = Builders<Resource>.Filter.Eq(x => x.Id, gatewayId);
            var result = await _resourceCollection.DeleteOneAsync(filter);
            return result.IsAcknowledged;
        }

        /// <inheritdoc/>
        public async Task<Resource> DeleteSegment(string gatewayId, string segmentId)
        {
            var response = await this.GetGateway(gatewayId);
            var gateway = response.Gateway;

            var segmentsToUpdate =  this.DeleteSegment(segmentId, gateway.Segments);
            gateway.Segments = segmentsToUpdate;

            await this.AddGatewaySegments(gatewayId, segmentsToUpdate);

            return gateway;
        }



        /// <summary>
        /// Loop throught every root segment and pass each to the FindSegementById function
        /// If segment with certain id found then BasePath, RequiresAuthentication and ResourceName will be changed
        /// to the correspondent values of segment
        /// </summary>
        /// <param name="segment">Segment to update.</param>
        /// <param name="resourceSegments">List of segment to search within.</param>
        /// <returns>List of resource segments with updated segment.</returns>
        private List<ResourceSegment> UpdateSegment(ResourceSegment segment, List<ResourceSegment> resourceSegments)
        {
            foreach (var resourceSegment in resourceSegments)
            {
                var segmentFound = this.FindSegementById(segment.SegmentId, resourceSegment);
                if (segmentFound != null)
                {
                    segmentFound.BasePath = segment.BasePath;
                    segmentFound.RequiresAuthentication = segment.RequiresAuthentication;
                    segmentFound.ResourceName = segment.ResourceName;
                    break;
                }
            }

            return resourceSegments;
        }

        // Loop throught every root segment and pass each to the FindSegementById function
        /// <summary>
        /// Loop throught every root segment and pass each to the FindSegementById function along with parent id.
        /// If segment with certain id found then segmentToAdd object will be added to the array object of parent id.
        /// </summary>
        /// <param name="parentId">Parent Id.</param>
        /// <param name="segmentToAdd">Segment to add.</param>
        /// <param name="resourceSegments">ist of segment to search within.</param>
        /// <returns>List of resource segments with updated segment.</returns>
        private List<ResourceSegment> AddSegmentToParent(string parentId, ResourceSegment segmentToAdd, List<ResourceSegment> resourceSegments)
        {
            foreach (var rootSegment in resourceSegments) 
            {
                var segmentFound = this.FindSegementById(parentId, rootSegment);
                if (segmentFound != null)
                {
                    segmentFound.ChildSegments.Add(segmentToAdd);
                    break;
                }
            }

            return resourceSegments;
        }

        /// <summary>
        /// Recursively searches for the segment with segment id equals to passed segment id. 
        /// </summary>
        /// <param name="segmentId">Segment Id.</param>
        /// <param name="resourceSegment">Resource Segment to search within.</param>
        /// <returns>Resource segment.</returns>
        private ResourceSegment FindSegementById(string segmentId, ResourceSegment resourceSegment)
        {
            if(resourceSegment.SegmentId == segmentId)
            {
                return resourceSegment;
            }

            foreach(var segment in resourceSegment.ChildSegments)
            {
                var result = this.FindSegementById(segmentId, segment);
                if(result != null)
                {
                    return result;
                }
            }

            return null;
        }


        /// <summary>
        /// Loop throught every root segment and pass each segment to the DeleteSegmentById function
        /// </summary>
        /// <param name="segmentId">Segment id to search for.</param>
        /// <param name="resourceSegments">List of resource segments.</param>
        /// <returns>List of resource segments with deleted segmnent.</returns>
        private List<ResourceSegment> DeleteSegment(string segmentId, List<ResourceSegment> resourceSegments)
        {
            foreach (var rootSegment in resourceSegments)
            {
                var segmentFound = this.DeleteSegmentById(segmentId, rootSegment, resourceSegments);
                if (segmentFound != null)
                {
                    break;
                }
            }

            return resourceSegments;
        }

        /// <summary>
        /// Recursively searches using DFS and deletes the segment. Also, Recursively passes the parent segments.
        /// </summary>
        /// <param name="segmentId">Segment id to look for.</param>
        /// <param name="resourceSegment">Resource segment to search within.</param>
        /// <param name="parentSegments">Parent Segments.</param>
        /// <returns>Deleted resource segment.</returns>
        private ResourceSegment DeleteSegmentById(string segmentId, ResourceSegment resourceSegment, List<ResourceSegment> parentSegments)
        {
            if(resourceSegment.SegmentId == segmentId)
            {
                parentSegments.RemoveAll(x => x.SegmentId == segmentId);
                return resourceSegment;
            }

            foreach (var segment in resourceSegment.ChildSegments)
            {
                var result = this.DeleteSegmentById(segmentId, segment, resourceSegment.ChildSegments);
                if(result != null)
                {
                    return result;
                }
            }

            return null;
        }
    }
}
