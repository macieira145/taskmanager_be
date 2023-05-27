using CardExchange.Entities;
using CardExchange.Interfaces;

namespace CardExchange.Repository
{
    public class UserRepository : IUserRepository
    {
        private TaskmanagerContext _context;
        public UserRepository(TaskmanagerContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Finds an user by its ID
        /// </summary>
        /// <param name="id">Id to find</param>
        /// <returns>Returns the user if found</returns>
        public User GetUser(int id)
        {
            return _context.Users.Where(u => u.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// Return all users in DB
        /// </summary>
        /// <returns>Return a list of users</returns>
        public ICollection<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        /// <summary>
        /// Checks if user exists
        /// </summary>
        /// <param name="id">Id to check</param>
        /// <returns>Return true if exists or false if it does not exist</returns>
        public bool UserExists(int id)
        {
            return _context.Users.Any(u => u.Id == id);
        }
    }
}
