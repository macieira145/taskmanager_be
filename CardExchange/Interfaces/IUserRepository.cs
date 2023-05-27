using CardExchange.Entities;

namespace CardExchange.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUser(int id);
        bool UserExists(int id);
    }
}
