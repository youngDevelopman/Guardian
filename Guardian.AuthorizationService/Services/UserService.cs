using Guardian.AuthorizationService;
using Guardian.AuthorizationService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guardian.Services.AuthorizationService
{
    public class UserService : IUserService
    {
        private AuthorizationServiceDbContext _context;
        public UserService(AuthorizationServiceDbContext context)
        {
            _context = context;
        }

        public async Task<User> AuthenticateAsync(Guid userPoolId, string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var userPool = _context.PoolUsers.Where(x => x.UserPoolId == userPoolId);

            User user = await userPool.SelectMany(x => _context.Users.Where(u => u.UserId == x.UserId && u.Username == username)).FirstOrDefaultAsync();
            
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

        public async Task<User> CreateUserAsync(User user, string password)
        {
            if(string.IsNullOrWhiteSpace(password))
                throw new Exception("Password is required");

            if (await _context.Users.AnyAsync(x => x.Username == user.Username))
                throw new Exception("Username \"" + user.Username + "\" is already taken");

            string passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> IsUserBelongsToPoolAsync(Guid userId, Guid userPoolId)
        {
            var user  = await _context.PoolUsers
                .Where(u => u.UserId == userId && u.UserPoolId == userPoolId)
                .FirstOrDefaultAsync();

            if(user == null)
            {
                return false;
            }

            return true;
        }

        public async Task<List<UserPool>> GetUserPoolsAsync()
        {
            var userPools = await _context.UserPool.ToListAsync();
            return userPools;
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
