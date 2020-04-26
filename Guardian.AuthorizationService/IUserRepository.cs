using System;
using Guardian.AuthorizationService.Models;

namespace Guardian.AuthorizationService
{
    public interface IUserRepository
    {
        public User CreateUser(User user, string password);
        public User Authenticate(Guid userPoolId, string username, string password);
    }
}