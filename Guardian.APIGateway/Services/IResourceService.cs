using Guardian.Shared.Models;
using Guardian.Shared.Models.AuthorizationService.Request;
using Guardian.Shared.Models.ResourceService.Response;
using System.Threading.Tasks;

namespace Guardian.APIGateway.Services
{
    public interface IResourceService
    {
        Task<GetResourceResponse> GetResource(GetResourceRequest request);
    }
}
