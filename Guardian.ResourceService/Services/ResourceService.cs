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

        public async Task<GetGatewaysResponse> GetGateways()
        {
            var filter = Builders<Resource>.Filter.Empty;
            var projection = Builders<Resource>.Projection.Expression(p => 
                new GatewayShortInfo { Name = p.Name, CreationDate = p.CreationDate, Description = p.Description, GatewayId = p.Id });
            var options = new FindOptions<Resource, GatewayShortInfo>();
            options.Projection = projection;

            var cursor = await _resourceCollection.FindAsync(filter, options);

            var resourceList = await cursor.ToListAsync();

            var response = new GetGatewaysResponse()
            {
                Gateways = resourceList
            };

            return response;
        }

        public async Task<GetResourceResponse> GetResource(GetResourceRequest request)
        {
            // Split user requested url by shashes into array of segments.
            var segments = request.RelativePath.Split('/')
                .Select(x => string.Concat('/', x))
                .ToList();

            var filter = Builders<Resource>.Filter.Eq(x => x.Domain, request.Domain);

            var cursor = await _resourceCollection.FindAsync<Resource>(filter);

            var resourceList = await cursor.ToListAsync();

            var resource = resourceList.FirstOrDefault();
            
            var urlTreeSearch = new UrlTreeSearch();

            Destination destination = urlTreeSearch.GenerateProxyDestination(resource.Segments, segments);

            var resourceServiceResponse = new GetResourceResponse()
            {
                UserPoolId = Guid.NewGuid(),
                IsAuthenticationRequired = destination.RequiresAuthentication,
                IsProxyDefined = true,
                SourceUrl = request.Domain + request.RelativePath,
                ProxyUrl = destination.FullPath
            };

            return resourceServiceResponse;
        }

        public async Task AddGateway(Resource resource)
        {
            await _resourceCollection.InsertOneAsync(resource);
        }

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

        public async Task<Resource> AddRootSegment(string gatewayId, AddRootSegmentRequest request)
        {
            //request.Segment.SegmentId = ObjectId.GenerateNewId().ToString();
            //request.Segment.ChildSegments = new List<ResourceSegment>();

            var filter = Builders<Resource>.Filter.Eq(x => x.Id, gatewayId);
            var update = Builders<Resource>.Update.Push<ResourceSegment>(x => x.Segments, null); //request.Segment);

            await _resourceCollection.FindOneAndUpdateAsync(filter, update);
            var result = await this.GetGateway(gatewayId);
            
            return result.Gateway;
        }

        public async Task<Resource> AddChildSegment(string gatewayId, AddChildSegmentRequest request)
        {
            //request.Segment.SegmentId = ObjectId.GenerateNewId().ToString();
            //request.Segment.ChildSegments = new List<ResourceSegment>();

            var response = await this.GetGateway(gatewayId);
            var gateway = response.Gateway;

            var segmentsToUpdate = this.AddSegmentToParent(request.ParentSegmentId,null /*request.Segment*/, gateway.Segments);
            gateway.Segments = segmentsToUpdate;

            //var updateGatewayRequest = new UpdateGatewayRequest()
            //{
            //    GatewayToUpdate = gateway
            //};

            //await this.UpdateGateway(updateGatewayRequest);

            return gateway;
        }

        public async  Task<bool> DeleteGateway(string gatewayId)
        {
            var filter = Builders<Resource>.Filter.Eq(x => x.Id, gatewayId);
            var result = await _resourceCollection.DeleteOneAsync(filter);
            return result.IsAcknowledged;
        }

        public async Task<Resource> DeleteSegment(string gatewayId, string segmentId)
        {
            var response = await this.GetGateway(gatewayId);
            var gateway = response.Gateway;

            var segmentsToUpdate =  this.DeleteSegment(segmentId, gateway.Segments);
            gateway.Segments = segmentsToUpdate;

            //var updateGatewayRequest = new UpdateGatewayRequest()
            //{
            //    GatewayToUpdate = gateway
            //};

            //await this.UpdateGateway(updateGatewayRequest);

            return gateway;
        }

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
