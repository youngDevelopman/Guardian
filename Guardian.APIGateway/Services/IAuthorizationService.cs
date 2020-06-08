using Guardian.Shared.Models;
using Guardian.Shared.Models.AuthorizationService.Request;
using System.Threading.Tasks;

namespace Guardian.APIGateway.Services
{
    public interface IAuthorizationService
    {
        Task<bool> ValidateToken(TokenValidationRequest request);
    }
}
