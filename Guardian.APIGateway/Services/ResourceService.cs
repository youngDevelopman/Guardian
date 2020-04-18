using Guardian.APIGateway.Models;
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
        public ResourceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResourceServiceResponse> GetResource(ResourceServiceRequest request)
        {
            var serializedObj = JsonConvert.SerializeObject(request);

            var httpResponse = await _httpClient.PostAsync(_resourcesUrl, new StringContent(serializedObj, Encoding.Default, "application/json"));
            var resourceServiceResponse = JsonConvert.DeserializeObject<ResourceServiceResponse>(await httpResponse.Content.ReadAsStringAsync());

            return resourceServiceResponse;
        }
    }
}
