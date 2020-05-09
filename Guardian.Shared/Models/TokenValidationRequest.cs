using Newtonsoft.Json;
using System;

namespace Guardian.Shared.Models
{
    public class TokenValidationRequest
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        public Guid UserPoolId { get; set; }
    }
}
