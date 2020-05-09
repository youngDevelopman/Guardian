using Newtonsoft.Json;
using System;

namespace Guardian.Models.AuthorizationService
{
    /// <summary>
    /// Security token format defined by OAuth v2 specification.
    /// </summary>
    public class OAuthTokenModel
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public DateTime ExpiresIn { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}
