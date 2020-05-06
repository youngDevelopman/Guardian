using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guardian.ResourceService.Models
{
    public class ResourceBranch
    {
        [BsonElement("resourceName")]
        public string ResourceName { get; set; }

        [BsonElement("basePath")]
        public string BasePath { get; set; }

        [BsonElement("requiresAuthentication")]
        public bool RequiresAuthentication { get; set; }

        [BsonElement("childBranches")]
        public List<ResourceBranch> ChildBranches { get; set; }
    }
}
