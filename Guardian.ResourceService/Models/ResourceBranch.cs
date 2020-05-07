using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guardian.ResourceService.Models
{
    public class ResourceSegment
    {
        [BsonElement("resourceName")]
        public string ResourceName { get; set; }

        [BsonElement("basePath")]
        public string BasePath { get; set; }

        [BsonElement("requiresAuthentication")]
        public bool RequiresAuthentication { get; set; }

        [BsonElement("childSegments")]
        public List<ResourceSegment> ChildSegments { get; set; }
    }
}
