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

        public async Task<DomainUserPool> GetUserPoolFromDomain(string domain)
        {
            var filter = Builders<Resource>.Filter.Eq(x => x.Domain, domain);

            var cursor = await _resourceCollection.FindAsync<Resource>(filter);

            var resourceList = await cursor.ToListAsync();

            var resource = resourceList.FirstOrDefault();

            var response = new DomainUserPool()
            {
                Domain = resource.Domain,
                UserPoolId = resource.UserPoolId
            };

            return response;
        }

        public async Task<ResourceServiceResponse> GetResource(ResourceServiceRequest request)
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

            var resourceServiceResponse = new ResourceServiceResponse()
            {
                UserPoolId = Guid.NewGuid(),
                IsAuthenticationRequired = destination.RequiresAuthentication,
                IsProxyDefined = true,
                SourceUrl = request.Domain + request.RelativePath,
                ProxyUrl = destination.FullPath
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
