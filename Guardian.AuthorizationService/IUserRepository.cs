using System;
using Guardian.AuthorizationService.Models;

namespace Guardian.AuthorizationService
{
    public interface IUserService
    {
        public User CreateUser(User user, string password);

        bool ValidateUserDomain(Guid userId, string domain);

        bool IsUserBelongsToPool(Guid userId, Guid userPoolId);

        public User Authenticate(Guid userPoolId, string username, string password);
    }
}