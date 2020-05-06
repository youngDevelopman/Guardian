using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Guardian.ResourceService.Models
{
    public class Resource
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("userPoolId")]
        public Guid UserPoolId { get; set; }

        [BsonElement("domain")]
        public string Domain { get; set; }

        [BsonElement("destination")]
        public Destination Destination { get; set; }

        [BsonElement("resources")]
        public List<ResourceBranch> Resources { get; set; }
    }
}
