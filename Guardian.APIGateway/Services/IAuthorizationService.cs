using Guardian.Shared.Models;
using System.Threading.Tasks;

namespace Guardian.APIGateway.Services
{
    public interface IAuthorizationService
    {
        Task<bool> ValidateToken(TokenValidationRequest request);
    }
}
