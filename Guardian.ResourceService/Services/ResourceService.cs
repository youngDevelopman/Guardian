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
        private readonly IMongoCollection<ResourceModel> _resourceCollection;

        public ResourceService(IMongoDatabase mongoDatabase)
        {
            _resourceCollection = mongoDatabase.GetCollection<ResourceModel>("resources");
        }

        public async Task<IEnumerable<ResourceModel>> GetResources()
        {
            var filter = Builders<ResourceModel>.Filter.Empty;

            var cursor = await _resourceCollection.FindAsync<ResourceModel>(filter);

            var list = await cursor.ToListAsync();

            return list;
        }
    }
}
