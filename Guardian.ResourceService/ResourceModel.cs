using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Guardian.ResourceService
{
    public class ResourceModel
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("endpoint")]
        public string Endpoint { get; set; }

        [BsonElement("destination")]
        public DestinationResourceModel Destination { get; set; }

        [BsonElement("resourceBranches")]
        public List<ResourceModel> ResourceBranches { get; set; }
    }

    public class DestinationResourceModel
    {
        [BsonElement("uri")]
        public string Uri { get; set; }

        [BsonElement("requiresAuthentication")]
        public bool RequiresAuthentication { get; set; }
    }
}
