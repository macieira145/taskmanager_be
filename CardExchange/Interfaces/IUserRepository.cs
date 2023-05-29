using CardExchange.Entities;

namespace CardExchange.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUser(int id);
        User GetUserByEmail(string email);
        bool UserExists(int id);
        bool CreateUser(User user);
        bool Save();
    }
}
