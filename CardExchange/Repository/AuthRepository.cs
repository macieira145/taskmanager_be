using CardExchange.Entities;
using CardExchange.Interfaces;
using System.Configuration;

namespace CardExchange.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private TaskmanagerContext _context;
        private IUserRepository _userRepository;
        private IConfiguration _configuration;

        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_context);
                }

                return _userRepository;
            }
        }

        public AuthRepository(TaskmanagerContext context)
        {
            _context = context;
        }

        public bool Register(User user)
        {
            _context.Users.Add(user);
            return _context.SaveChanges() == 1 ? true : false;
        }

        // checks if user exists in database
        public bool UserExists(string email)
        {
            if(_context.Users.Any(u => u.Email.ToLower() == email.ToLower()))
            {
                return true;
            }
            return false;
        }
    }
}
