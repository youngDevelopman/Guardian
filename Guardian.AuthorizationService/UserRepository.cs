using Guardian.AuthorizationService.Models;
using System;
using System.Linq;

namespace Guardian.AuthorizationService
{
    public class UserService : IUserService
    {
        private AuthorizationServiceDbContext _context;
        public UserService(AuthorizationServiceDbContext context)
        {
            _context = context;
        }

        public User Authenticate(Guid userPoolId, string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var userPool = _context.UserPool.SingleOrDefault(x => x.UserPoolId == userPoolId);
            User user = null;
            
            if(userPool != null)
            {
                user = _context.Users.SingleOrDefault(x => x.Username == username);
            }

            if (user == null)
                return null;

            if (!VerifyPasswordHash(
                password, 
                Convert.FromBase64String(user.PasswordHash),
                Convert.FromBase64String(user.PasswordSalt)))
            {
                return null;
            }

            return user;
        }

        public User CreateUser(User user, string password)
        {
            if(string.IsNullOrWhiteSpace(password))
                throw new Exception("Password is required");

            if (_context.Users.Any(x => x.Username == user.Username))
                throw new Exception("Username \"" + user.Username + "\" is already taken");

            string passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public bool IsUserBelongsToPool(Guid userId, Guid userPoolId)
        {
            var user  = _context.PoolUsers
                .Where(u => u.UserId == userId && u.UserPoolId == userPoolId)
                .FirstOrDefault();

            if(user == null)
            {
                return false;
            }

            return true;
        }

        public bool ValidateUserDomain(Guid userId, string domain)
        {
            var userPoolDomain = _context.UserPoolDomain
                .Where(u => u.Domain == domain)
                .FirstOrDefault();

            if (userPoolDomain == null)
            {
                return false;
            }

            bool isBelongsToPool = this.IsUserBelongsToPool(userId, userPoolDomain.UserPoolId);

            return isBelongsToPool;
        }

        private static void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = Convert.ToBase64String(hmac.Key);
                passwordHash = Convert.ToBase64String(hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
