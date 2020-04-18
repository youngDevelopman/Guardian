using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guardian.ResourceService.Models
{
    public class ResourceServiceResponse
    {
        public Guid UserPoolId { get; set; }
        public string SourceUrl { get; set; }
        public string ProxyUrl { get; set; }
        public bool IsProxyDefined { get; set; }
        public bool IsAuthenticationRequired { get; set; }
    }
}
