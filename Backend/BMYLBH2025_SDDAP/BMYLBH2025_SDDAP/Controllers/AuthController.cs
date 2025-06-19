using BMYLBH2025_SDDAP.Models;
using System;
using System.Web.Http;

namespace BMYLBH2025_SDDAP.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        private readonly IUserRepository _userRepository;
        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login([FromBody] LoginRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Email and password required.");
            }

            var user = _userRepository.GetByEmail(request.Email);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            if (user.Password != request.Password)
            {
                return BadRequest("Incorrect password.");
            }

            var token = Guid.NewGuid().ToString();
            TokenStore.Tokens[token] = user.UserID; // Store token with user ID

            return Ok(new { token });
        }

    }
}
