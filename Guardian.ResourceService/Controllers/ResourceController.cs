using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Guardian.ResourceService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResourceController : ControllerBase
    {
        IMongoDatabase _database;
        public ResourceController()
        {
            string connectionString = "mongodb://localhost:27017";
            MongoClient client = new MongoClient(connectionString);
            _database = client.GetDatabase("guardian");
        }
        public async Task<IActionResult> Index()
        {
           var collection = _database.GetCollection<ResourceModel>("resources");
           var filter = Builders<ResourceModel>.Filter.Empty;

           var cursor  = await collection.FindAsync<ResourceModel>(filter);
           
           var list = await cursor.ToListAsync();


           return Ok(list);
        }
    }
}