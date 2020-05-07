using Guardian.APIGateway.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Guardian.APIGateway.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly HttpClient _httpClient;
        private const string _validateTokenUrl = "/validate";
        public AuthorizationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> ValidateToken(ValidationRequest request)
        {
            var serializedObj = JsonConvert.SerializeObject(request);

            var httpResponse = await _httpClient.PostAsync(_validateTokenUrl, new StringContent(serializedObj, Encoding.Default, "application/json"));

            if (!httpResponse.IsSuccessStatusCode)
            {
                return false;
            }

            return true;
        }
    }
}
