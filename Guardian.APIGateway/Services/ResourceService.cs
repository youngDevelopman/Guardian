﻿using Guardian.Shared.Models;
using Guardian.Shared.Models.AuthorizationService.Request;
using Guardian.Shared.Models.ResourceService.Response;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Guardian.APIGateway.Services
{
    public class ResourceService : IResourceService
    {
        private readonly HttpClient _httpClient;
        private const string _resourcesUrl = "/resources";
        private const string _domainUserPoolUrl = "/resources/domain-user-pool";
        public ResourceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GetResourceResponse> GetResource(GetResourceRequest request)
        {
            var serializedObj = JsonConvert.SerializeObject(request);

            var httpResponse = await _httpClient.PostAsync(_resourcesUrl, new StringContent(serializedObj, Encoding.Default, "application/json"));
            var resourceServiceResponse = JsonConvert.DeserializeObject<GetResourceResponse>(await httpResponse.Content.ReadAsStringAsync());

            return resourceServiceResponse;
        }
    }
}
