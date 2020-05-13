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
    }
}
