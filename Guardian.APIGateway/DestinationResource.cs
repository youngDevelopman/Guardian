using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Guardian.APIGateway
{
    public class DestinationResource
    {
        public string Uri { get; set; }
        public bool RequiresAuthentication { get; set; }

        static HttpClient client = new HttpClient();

        public DestinationResource(string uri, bool requiresAuthentication)
        {
            Uri = uri;
            RequiresAuthentication = requiresAuthentication;
        }

        public DestinationResource(string uri)
           : this(uri, false)
        {
        }

        private DestinationResource()
        {
            Uri = "/";
            RequiresAuthentication = false;
        }

        public async Task<HttpResponseMessage> SendRequest(HttpRequest request)
        {
            string requestContent;
            using (Stream receiveStream = request.Body)
            {
                using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    requestContent = await readStream.ReadToEndAsync();
                }
            }

            using (var newRequest = new HttpRequestMessage(new HttpMethod(request.Method), this.Uri))
            {
                newRequest.Content = new StringContent(requestContent, Encoding.UTF8, request.ContentType);
                var response = await client.SendAsync(newRequest);
                return response;
            }
        }
    }
}
