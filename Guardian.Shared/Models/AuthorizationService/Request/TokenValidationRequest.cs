using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Guardian.Shared.Models.AuthorizationService.Request
{
    public class TokenValidationRequest
    {
        [Required]
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [Required]
        public Guid UserPoolId { get; set; }
    }
}
