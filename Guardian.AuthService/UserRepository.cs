using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guardian.AuthorizationService
{
    public class UserRepository
    {
        public List<User> TestUsers;
        public UserRepository()
        {
            TestUsers = new List<User>();
            TestUsers.Add(new User() { UserId = Guid.Parse("4804cc1d-b0b8-46b0-90dd-a611ae614e98"), Username = "Test1", Password = "Pass1" });
            TestUsers.Add(new User() { UserId = Guid.Parse("ec169de9-be43-476e-9dd9-5f9051cef31a"), Username = "Test2", Password = "Pass2" });
        }

        public User GetUser(string username)
        {
            try
            {
                return TestUsers.First(user => user.Username.Equals(username));
            }
            catch
            {
                return null;
            }
        }
    }
}
