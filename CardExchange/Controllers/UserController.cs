using CardExchange.Entities;
using CardExchange.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CardExchange.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Gets the list of users from DB
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetUsers()
        {
            var users = _userRepository.GetUsers();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(users);
        }


        /// <summary>
        /// End-point to getuser by its ID
        /// </summary>
        /// <param name="userId">ID of the user to get</param>
        /// <returns>Returns the user</returns>
        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUser(int userId) 
        {
            // checks if user exists
            if (!_userRepository.UserExists(userId))
                return NotFound();

            // gets the user 
            var user = _userRepository.GetUser(userId);

            // checks if user meets all the requirements
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            // return the user 
            return Ok(user);
        }
    }
}
