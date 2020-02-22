using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guardian.AuthorizationService
{
    /// <summary>
    /// Security token format defined by OAuth v2 specification.
    /// </summary>
    public class OAuthTokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public DateTime ExpiresIn { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}
