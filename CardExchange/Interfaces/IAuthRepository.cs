using CardExchange.Entities;

namespace CardExchange.Interfaces
{
    public interface IAuthRepository
    { 
        IUserRepository UserRepository { get; }
        bool Register(User user);
        bool UserExists(string email);
    }
}
