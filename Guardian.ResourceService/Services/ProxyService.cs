using Guardian.ResourceService.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guardian.ResourceService.Services
{
    public class ProxyService : IProxyService
    { 
        // Collection (table) that stores information about endpoints
        private readonly IMongoCollection<Resource> _resourceCollection;

        public ProxyService(IMongoDatabase mongoDatabase)
        {
            _resourceCollection = mongoDatabase.GetCollection<Resource>("resources");
        }

        public async Task<ProxyResource> GenerateProxy(string domain, string relativePath)
        {
            // Split user requested url by shashes into array of segments.
            var segments = relativePath.Split('/')
                .Select(x => string.Concat('/', x))
                .ToList();

            var filter = Builders<Resource>.Filter.Eq(x => x.Domain, domain);

            var cursor = await _resourceCollection.FindAsync<Resource>(filter);

            var resourceList = await cursor.ToListAsync();

            var resource = resourceList.FirstOrDefault();

            var urlTreeSearch = new UrlTreeSearch();

            Destination destination = urlTreeSearch.GenerateProxyDestination(resource.Segments, segments);

            var proxy = new ProxyResource()
            {
                UserPoolId = Guid.NewGuid(),
                IsAuthenticationRequired = destination.RequiresAuthentication,
                IsProxyDefined = true,
                SourceUrl = domain + relativePath,
                ProxyUrl = destination.FullPath
            };

            return proxy;
        }
    }
}
