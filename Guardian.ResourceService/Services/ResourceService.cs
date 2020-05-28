using Guardian.ResourceService.Models;
using Guardian.Shared.Models;
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

        public async Task<bool> UpdateGateway(UpdateGatewayRequest request)
        {
            var result = await _resourceCollection.ReplaceOneAsync(x => x.Id == request.GatewayToUpdate.Id, request.GatewayToUpdate);
            
            if(!result.IsAcknowledged)
            {
                return false;
            }

            return true;
        }
    }
}
