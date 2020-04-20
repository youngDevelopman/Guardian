using Guardian.ResourceService.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guardian.ResourceService.Services
{
    public class ResourceService : IResourceService
    {
        // Collection (table) that stores information about endpoints
        private readonly IMongoCollection<ResourceModel> _resourceCollection;

        public ResourceService(IMongoDatabase mongoDatabase)
        {
            _resourceCollection = mongoDatabase.GetCollection<ResourceModel>("resources");
        }

        public async Task<ResourceModel> GetResource(ResourceServiceRequest request)
        {
            //string[] parts = Regex.Split(request.Path, @"(?<=[/])");
            var segments = request.Path.Split('/')
                .Select(x => string.Concat('/', x))
                .ToList();
            // TODO: First, find user pool id that assosiated with request.BasePath
            string rootEndpoint = segments.First();

            var filter = Builders<ResourceModel>.Filter.Eq(x => x.Endpoint, rootEndpoint);

            var cursor = await _resourceCollection.FindAsync<ResourceModel>(filter);

            var resourceList = await cursor.ToListAsync();

            var urlTreeSearch = new UrlTreeSearch();
            urlTreeSearch.IsPathExists(resourceList, segments);

            return resourceList.First();
        }

        public async Task<IEnumerable<ResourceModel>> GetResources(ResourceServiceRequest request)
        {
            var filter = Builders<ResourceModel>.Filter.Empty;

            var cursor = await _resourceCollection.FindAsync<ResourceModel>(filter);

            var list = await cursor.ToListAsync();

            return list;
        }
    }
}
