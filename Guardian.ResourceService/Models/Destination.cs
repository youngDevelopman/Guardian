using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guardian.ResourceService.Models
{
    public class Destination
    {
        [BsonElement("uri")]
        public string Uri { get; set; }

        [BsonElement("requiresAuthentication")]
        public bool RequiresAuthentication { get; set; }
    }
}
