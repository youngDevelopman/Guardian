using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guardian.APIGateway.Services
{
    public interface IAuthorizationService
    {
        Task<bool> ValidateToken(string token);
    }
}
