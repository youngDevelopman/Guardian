using System;
using System.Collections.Generic;
using System.Text;

namespace Guardian.Shared.Models.ResourceService.Response
{
    public class GetResourceResponse
    {
        public Guid UserPoolId { get; set; }
        public string SourceUrl { get; set; }
        public string ProxyUrl { get; set; }
        public bool IsProxyDefined { get; set; }
        public bool IsAuthenticationRequired { get; set; }
    }
}
