using System;
using System.Web.Http;
using BMYLBH2025_SDDAP.Models;
using BMYLBH2025_SDDAP.Services;
using System.Threading.Tasks;

namespace BMYLBH2025_SDDAP.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        public AuthController(IUserRepository userRepository, IEmailService emailService)
        {
            _userRepository = userRepository;
            _emailService = emailService;
        }

        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login([FromBody] LoginRequest request)
        {
            try
            {
                if (request == null || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
                {
                    return BadRequest("Email and password are required.");
                }

                var user = _userRepository.GetByEmail(request.Email);
                
                if (user == null || !VerifyPassword(request.Password, user.PasswordHash))
                {
                    return Unauthorized();
                }

                if (!user.IsEmailVerified)
                {
                    return BadRequest("Please verify your email address before logging in.");
                }

                // Generate a simple token (in production, use JWT)
                var token = GenerateToken(user);

                return Ok(new { Token = token, Message = "Login successful" });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<IHttpActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                if (request == null || string.IsNullOrEmpty(request.Email) || 
                    string.IsNullOrEmpty(request.Password) || string.IsNullOrEmpty(request.FullName))
                {
                    return BadRequest("All fields are required.");
                }

                // Check if user already exists
                var existingUser = _userRepository.GetByEmail(request.Email);
                if (existingUser != null)
                {
                    return BadRequest("User with this email already exists.");
                }

                // Create new user
                var user = new User
                {
                    Email = request.Email.ToLower(),
                    FullName = request.FullName,
                    PasswordHash = HashPassword(request.Password),
                    Role = "User",
                    IsEmailVerified = false,
                    EmailVerificationToken = Guid.NewGuid().ToString(),
                    CreatedAt = DateTime.Now
                };

                var userId = _userRepository.Create(user);
                
                if (userId > 0)
                {
                    // Send verification email
                    await _emailService.SendVerificationEmailAsync(user.Email, user.EmailVerificationToken);
                    
                    // Send welcome email
                    await _emailService.SendWelcomeEmailAsync(user.Email, user.FullName);

                    return Ok(new { 
                        Message = "Registration successful! Please check your email to verify your account.",
                        UserId = userId 
                    });
                }

                return InternalServerError();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("verify-email")]
        public IHttpActionResult VerifyEmail(string token)
        {
            try
            {
                if (string.IsNullOrEmpty(token))
                {
                    return BadRequest("Verification token is required.");
                }

                var user = _userRepository.GetByVerificationToken(token);
                
                if (user == null)
                {
                    return BadRequest("Invalid verification token.");
                }

                if (user.IsEmailVerified)
                {
                    return Ok(new { Message = "Email is already verified." });
                }

                // Update user verification status
                user.IsEmailVerified = true;
                user.EmailVerificationToken = null;
                _userRepository.Update(user);

                return Ok(new { Message = "Email verified successfully! You can now log in." });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("resend-verification")]
        public async Task<IHttpActionResult> ResendVerificationEmail([FromBody] ResendVerificationRequest request)
        {
            try
            {
                if (request == null || string.IsNullOrEmpty(request.Email))
                {
                    return BadRequest("Email is required.");
                }

                var user = _userRepository.GetByEmail(request.Email);
                
                if (user == null)
                {
                    return BadRequest("User not found.");
                }

                if (user.IsEmailVerified)
                {
                    return BadRequest("Email is already verified.");
                }

                // Generate new token if needed
                if (string.IsNullOrEmpty(user.EmailVerificationToken))
                {
                    user.EmailVerificationToken = Guid.NewGuid().ToString();
                    _userRepository.Update(user);
                }

                // Resend verification email
                await _emailService.SendVerificationEmailAsync(user.Email, user.EmailVerificationToken);

                return Ok(new { Message = "Verification email sent successfully." });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("forgot-password")]
        public async Task<IHttpActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            try
            {
                if (request == null || string.IsNullOrEmpty(request.Email))
                {
                    return BadRequest("Email is required.");
                }

                var user = _userRepository.GetByEmail(request.Email);
                
                if (user == null)
                {
                    // Don't reveal whether user exists or not for security
                    return Ok(new { Message = "If the email exists, password reset instructions have been sent." });
                }

                // Generate password reset token
                user.PasswordResetToken = Guid.NewGuid().ToString();
                user.PasswordResetExpiry = DateTime.Now.AddHours(1); // 1 hour expiry
                _userRepository.Update(user);

                // Send password reset email (you can implement this template)
                var resetLink = $"https://localhost:44313/reset-password?token={user.PasswordResetToken}";
                var subject = "Password Reset - Inventory Management System";
                var body = $@"
                    <h2>Password Reset Request</h2>
                    <p>Click the link below to reset your password:</p>
                    <a href='{resetLink}'>Reset Password</a>
                    <p>This link will expire in 1 hour.</p>";

                await _emailService.SendEmailAsync(user.Email, subject, body, true);

                return Ok(new { Message = "If the email exists, password reset instructions have been sent." });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #region Helper Methods

        private string HashPassword(string password)
        {
            // Simple hash for demonstration - use BCrypt or similar in production
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password + "salt"));
        }

        private bool VerifyPassword(string password, string hash)
        {
            return HashPassword(password) == hash;
        }

        private string GenerateToken(User user)
        {
            // Simple token generation - use JWT in production
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{user.UserID}:{user.Email}:{DateTime.Now:yyyy-MM-dd}"));
        }

        #endregion
    }

    #region Request Models

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
    }

    public class ResendVerificationRequest
    {
        public string Email { get; set; }
    }

    public class ForgotPasswordRequest
    {
        public string Email { get; set; }
    }

    #endregion
}
