using CardExchange.Entities;
using CardExchange.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CardExchange.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserController(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
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

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateUser([FromBody] User userCreate)
        {
            // checks if user has data
            if(userCreate == null)
                return BadRequest(ModelState);

            // gets any user with the same email
            var user = _userRepository.GetUsers()
                .Where(u => u.Email == userCreate.Email).FirstOrDefault();

            // checks if user exists 
            if(user != null)
            {
                ModelState.AddModelError("", "User already exists");
                return StatusCode(422, ModelState);
            }

            // checks if data is valid
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            // tries to create the user, otherwise throws an error
            if(!_userRepository.CreateUser(userCreate))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
    }
}
