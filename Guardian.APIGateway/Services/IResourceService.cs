using Guardian.APIGateway.Models;
using Guardian.Shared.Models;
using System.Threading.Tasks;

namespace Guardian.APIGateway.Services
{
    public interface IResourceService
    {
        Task<GetResourceResponse> GetResource(GetResourceRequest request);
    }
}
