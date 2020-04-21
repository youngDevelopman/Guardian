using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Guardian.ResourceService.Models
{
    public class Resource
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("endpoint")]
        public string Endpoint { get; set; }

        [BsonElement("destination")]
        public Destination Destination { get; set; }

        [BsonElement("resourceBranches")]
        public List<Resource> ResourceBranches { get; set; }
    }

}
