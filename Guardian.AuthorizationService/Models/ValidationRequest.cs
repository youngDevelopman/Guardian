using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guardian.AuthorizationService.Models
{
    public class ValidationRequest
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        public Guid UserPoolId { get; set; }
    }
}
