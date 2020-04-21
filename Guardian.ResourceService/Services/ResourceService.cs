using Guardian.ResourceService.Models;
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

        public async Task<ResourceServiceResponse> GenerateProxy(ResourceServiceRequest request)
        {
            // Split user requested url by shashes into array of segments.
            var segments = request.Path.Split('/')
                .Select(x => string.Concat('/', x))
                .ToList();

            // TODO: First, find user pool id that assosiated with request.BasePath
            string rootEndpoint = segments.First();

            var filter = Builders<Resource>.Filter.Eq(x => x.Endpoint, rootEndpoint);

            var cursor = await _resourceCollection.FindAsync<Resource>(filter);

            var resourceList = await cursor.ToListAsync();

            var urlTreeSearch = new UrlTreeSearch();

            Destination destination = urlTreeSearch.GenerateProxyDestination(resourceList, segments);

            var resourceServiceResponse = new ResourceServiceResponse()
            {
                UserPoolId = Guid.NewGuid(),
                IsAuthenticationRequired = destination.RequiresAuthentication,
                IsProxyDefined = true,
                SourceUrl = request.BasePath + request.Path,
                ProxyUrl = destination.Uri
            };

            return resourceServiceResponse;
        }

        public async Task<IEnumerable<Resource>> GetResources(ResourceServiceRequest request)
        {
            var filter = Builders<Resource>.Filter.Empty;

            var cursor = await _resourceCollection.FindAsync<Resource>(filter);

            var list = await cursor.ToListAsync();

            return list;
        }
    }
}
