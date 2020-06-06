using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Guardian.AuthorizationService.Models;

namespace Guardian.Services.AuthorizationService
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(User user, string password);

        Task<bool> IsUserBelongsToPoolAsync(Guid userId, Guid userPoolId);

        Task<User> AuthenticateAsync(Guid userPoolId, string username, string password);

        Task<List<UserPool>> GetUserPoolsAsync();
    }
}