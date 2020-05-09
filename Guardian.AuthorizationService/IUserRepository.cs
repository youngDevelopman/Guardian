using System;
using Guardian.AuthorizationService.Models;

namespace Guardian.AuthorizationService
{
    public interface IUserService
    {
        public User CreateUser(User user, string password);

        bool IsUserBelongsToPool(Guid userId, Guid userPoolId);

        public User Authenticate(Guid userPoolId, string username, string password);
    }
}