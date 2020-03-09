using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

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
    }

    public class DestinationResourceModel
    {
        [BsonElement("uri")]
        public string DestinationUrl { get; set; }

        [BsonElement("requiresAuthentication")]
        public bool RequiresAuthentication { get; set; }
    }
}
