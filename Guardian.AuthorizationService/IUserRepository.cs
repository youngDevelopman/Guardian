namespace Guardian.AuthorizationService
{
    public interface IUserRepository
    {
        public User CreateUser(User user, string password);
        public User GetUser(string username);
    }
}