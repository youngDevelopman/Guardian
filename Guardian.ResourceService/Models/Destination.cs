using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guardian.ResourceService.Models
{
    public class Destination
    {
        public string FullPath { get; set; }

        public bool RequiresAuthentication { get; set; }
    }
}
