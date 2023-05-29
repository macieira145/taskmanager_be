using CardExchange.Dto;
using CardExchange.Entities;
using CardExchange.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CardExchange.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;

        public AuthController(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserAuthDTO userAuth)
        {
            // checks if user exists
            if (_authRepository.UserExists(userAuth.Email))
            {
                ModelState.AddModelError("", "User already exists");
                return StatusCode(422, ModelState);
            }

            // hashes the password
            CreatePasswordHash(userAuth.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User user = new User(userAuth);

            // hash and salt of password save
            user.PasswordHash = Convert.ToBase64String(passwordHash);
            user.PasswordSalt = Convert.ToBase64String(passwordSalt);

            if (!_authRepository.Register(user))
                return BadRequest(ModelState);

            return Ok("User created successfully;");
        }

        [HttpPost("login")]
        [ProducesResponseType(403)]
        public IActionResult Login([FromBody] UserAuthDTO userAuth)
        {
            // gets the user with the email received
            User user = _authRepository.UserRepository.GetUserByEmail(userAuth.Email);

            // validates the user
            if (user is null || !VerifyPasswordHash(userAuth.Password, Convert.FromBase64String(user.PasswordHash), Convert.FromBase64String(user.PasswordSalt)))
            {
                // throws a error message
                return StatusCode(403, new
                {
                    status = 403,
                    message = "Invalid credentials"
                });
            }

            // returns the generated access_token
            return Ok(new
            {
                status = 200,
                data = new
                {
                    access_token = CreateToken(user)
                }
            });
        }

        /// <summary>
        /// Hashes the user password into a sha512 hash
        /// </summary>
        /// <param name="passowrd">Password to be hashed</param>
        /// <param name="passwordHash">Password hashed</param>
        /// <param name="passwordSalt">Password salt used to hash</param>
        private void CreatePasswordHash(string passowrd, out byte[] passwordHash, out byte[] passwordSalt)
        {
            // uses SHA512 hasher
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passowrd));
            }
        }

        /// <summary>
        /// Verifies if the user password hash matches with db
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <param name="passwordSalt"></param>
        /// <returns></returns>
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                return computedHash.SequenceEqual(passwordHash);
            }
        }

        /// <summary>
        /// Creates a JWT token with the necessary info to perform a user auth
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private string CreateToken(User user)
        {
            // user claims with id and email (identifiers)
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email,ToString())
            };

            // config setup
            var appSettingsToken = _configuration.GetSection("AppSettings:Token").Value;
            if (appSettingsToken is null)
                throw new Exception("AppSettings Token is null!");

            // converts the string to bytes to perform the hashing
            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(appSettingsToken));

            // hashes the key with Sha512
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            // creates the token descriptor 
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = creds
            };

            // creates the token from the descriptor 
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
