using BMYLBH2025_SDDAP.Models;
using BMYLBH2025_SDDAP.Services;
using System;
using System.Threading.Tasks;
using System.Web.Http;

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
                    return Json(ApiResponse<LoginResponseData>.CreateError("Email and password are required.", "ValidationError"));
                }

                var user = _userRepository.GetByEmail(request.Email);

                if (user == null)
                {
                    var errorData = new LoginResponseData
                    {
                        Email = request.Email,
                        ShowForgotPassword = true
                    };
                    return Json(ApiResponse<LoginResponseData>.CreateError("Invalid email or password.", "InvalidCredentials", errorData));
                }

                user.PasswordHash = string.IsNullOrEmpty(user.PasswordHash) ? HashPassword(user.Password) : user.PasswordHash;

                if (!VerifyPassword(request.Password, user.PasswordHash))
                {
                    var errorData = new LoginResponseData
                    {
                        Email = request.Email,
                        ShowForgotPassword = true
                    };
                    return Json(ApiResponse<LoginResponseData>.CreateError("Invalid password.", "InvalidPassword", errorData));
                }

                if (!user.IsEmailVerified)
                {
                    var errorData = new LoginResponseData
                    {
                        Email = request.Email,
                        ShowResendVerification = true
                    };
                    return Json(ApiResponse<LoginResponseData>.CreateError("Please verify your email address before logging in.", "EmailNotVerified", errorData));
                }

                // Generate a simple token (in production, use JWT)
                var token = GenerateToken(user);

                var successData = new LoginResponseData
                {
                    Token = token,
                    Email = user.Email,
                    FullName = user.FullName,
                    Role = user.Role
                };

                return Json(ApiResponse<LoginResponseData>.CreateSuccess(successData, "Login successful"));
            }
            catch (Exception ex)
            {
                return Json(ApiResponse<LoginResponseData>.CreateError($"An error occurred: {ex.Message}", "ServerError"));
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
                    return Json(ApiResponse<RegisterResponseData>.CreateError("All fields are required.", "ValidationError"));
                }

                // Check if user already exists
                var existingUser = _userRepository.GetByEmail(request.Email);
                if (existingUser != null)
                {
                    return Json(ApiResponse<RegisterResponseData>.CreateError("User with this email already exists.", "UserExists"));
                }

                // Create new user
                var user = new User
                {
                    Username = request.Email.ToLower(), // Use email as username
                    Email = request.Email.ToLower(),
                    FullName = request.FullName,
                    PasswordHash = HashPassword(request.Password),
                    Role = "User",
                    IsEmailVerified = false,
                    EmailVerificationToken = Guid.NewGuid().ToString(),
                    CreatedAt = DateTime.Now
                };

                _userRepository.Create(user);

                // Send verification email
                await _emailService.SendVerificationEmailAsync(user.Email, user.EmailVerificationToken);

                // Send welcome email
                await _emailService.SendWelcomeEmailAsync(user.Email, user.FullName);

                var responseData = new RegisterResponseData
                {
                    UserId = user.UserID,
                    Email = user.Email
                };

                return Json(ApiResponse<RegisterResponseData>.CreateSuccess(responseData,
                    "Registration successful! Please check your email to verify your account."));
            }
            catch (Exception ex)
            {
                return Json(ApiResponse<RegisterResponseData>.CreateError($"Registration failed: {ex.Message}", "ServerError"));
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
                    return Json(ApiResponse<EmailResponseData>.CreateError("Verification token is required.", "ValidationError"));
                }

                var user = _userRepository.GetByVerificationToken(token);

                if (user == null)
                {
                    return Json(ApiResponse<EmailResponseData>.CreateError("Invalid verification token.", "InvalidToken"));
                }

                var responseData = new EmailResponseData
                {
                    Email = user.Email,
                    Action = "verification"
                };

                if (user.IsEmailVerified)
                {
                    return Json(ApiResponse<EmailResponseData>.CreateSuccess(responseData, "Email is already verified."));
                }

                // Update user verification status
                user.IsEmailVerified = true;
                user.EmailVerificationToken = null;
                _userRepository.Update(user);

                return Json(ApiResponse<EmailResponseData>.CreateSuccess(responseData, "Email verified successfully! You can now log in."));
            }
            catch (Exception ex)
            {
                return Json(ApiResponse<EmailResponseData>.CreateError($"Email verification failed: {ex.Message}", "ServerError"));
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
                    return Json(ApiResponse<EmailResponseData>.CreateError("Email is required.", "ValidationError"));
                }

                var user = _userRepository.GetByEmail(request.Email);

                if (user == null)
                {
                    return Json(ApiResponse<EmailResponseData>.CreateError("User not found.", "UserNotFound"));
                }

                if (user.IsEmailVerified)
                {
                    return Json(ApiResponse<EmailResponseData>.CreateError("Email is already verified.", "AlreadyVerified"));
                }

                // Generate new token if needed
                if (string.IsNullOrEmpty(user.EmailVerificationToken))
                {
                    user.EmailVerificationToken = Guid.NewGuid().ToString();
                    _userRepository.Update(user);
                }

                // Resend verification email
                await _emailService.SendVerificationEmailAsync(user.Email, user.EmailVerificationToken);

                var responseData = new EmailResponseData
                {
                    Email = user.Email,
                    Action = "verification"
                };

                return Json(ApiResponse<EmailResponseData>.CreateSuccess(responseData, "Verification email sent successfully."));
            }
            catch (Exception ex)
            {
                return Json(ApiResponse<EmailResponseData>.CreateError($"Failed to send verification email: {ex.Message}", "ServerError"));
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
                    return Json(ApiResponse<EmailResponseData>.CreateError("Email is required.", "ValidationError"));
                }

                var responseData = new EmailResponseData
                {
                    Email = request.Email,
                    Action = "password-reset"
                };

                var user = _userRepository.GetByEmail(request.Email);

                if (user == null)
                {
                    // Don't reveal whether user exists or not for security, but still return success
                    return Json(ApiResponse<EmailResponseData>.CreateSuccess(responseData,
                        "If the email exists, a password reset code has been sent."));
                }

                // Generate 6-digit OTP
                var otp = GenerateOTP();
                user.PasswordResetOTP = otp;
                user.OTPExpiry = DateTime.UtcNow.AddMinutes(15); // 15 minutes expiry
                _userRepository.Update(user);

                // Send OTP email
                await _emailService.SendPasswordResetOTPAsync(user.Email, otp, user.FullName);

                return Json(ApiResponse<EmailResponseData>.CreateSuccess(responseData,
                    "If the email exists, a password reset code has been sent."));
            }
            catch (Exception ex)
            {
                return Json(ApiResponse<EmailResponseData>.CreateError($"Failed to process password reset: {ex.Message}", "ServerError"));
            }
        }

        [HttpPost]
        [Route("verify-reset-otp")]
        public IHttpActionResult VerifyResetOTP([FromBody] VerifyOTPRequest request)
        {
            try
            {
                if (request == null || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.OTP))
                {
                    return Json(ApiResponse<OTPResponseData>.CreateError("Email and OTP are required.", "ValidationError"));
                }

                var user = _userRepository.GetByEmail(request.Email);

                if (user == null || user.PasswordResetOTP != request.OTP || user.OTPExpiry < DateTime.UtcNow)
                {
                    return Json(ApiResponse<OTPResponseData>.CreateError("Invalid or expired OTP.", "InvalidOTP"));
                }

                var responseData = new OTPResponseData
                {
                    Email = user.Email,
                    IsValid = true
                };

                return Json(ApiResponse<OTPResponseData>.CreateSuccess(responseData, "OTP verified successfully."));
            }
            catch (Exception ex)
            {
                return Json(ApiResponse<OTPResponseData>.CreateError($"OTP verification failed: {ex.Message}", "ServerError"));
            }
        }

        [HttpPost]
        [Route("reset-password")]
        public IHttpActionResult ResetPassword([FromBody] ResetPasswordRequest request)
        {
            try
            {
                if (request == null || string.IsNullOrEmpty(request.Email) ||
                    string.IsNullOrEmpty(request.OTP) || string.IsNullOrEmpty(request.NewPassword))
                {
                    return Json(ApiResponse<PasswordResetResponseData>.CreateError("All fields are required.", "ValidationError"));
                }

                var user = _userRepository.GetByEmail(request.Email);

                if (user == null || user.PasswordResetOTP != request.OTP || user.OTPExpiry < DateTime.UtcNow)
                {
                    return Json(ApiResponse<PasswordResetResponseData>.CreateError("Invalid or expired OTP.", "InvalidOTP"));
                }

                // Reset password
                user.PasswordHash = HashPassword(request.NewPassword);
                user.PasswordResetOTP = null;
                user.OTPExpiry = null;
                user.PasswordResetToken = null;
                user.PasswordResetExpiry = null;
                _userRepository.Update(user);

                var responseData = new PasswordResetResponseData
                {
                    Email = user.Email,
                    Success = true
                };

                return Json(ApiResponse<PasswordResetResponseData>.CreateSuccess(responseData,
                    "Password reset successfully. You can now log in with your new password."));
            }
            catch (Exception ex)
            {
                return Json(ApiResponse<PasswordResetResponseData>.CreateError($"Password reset failed: {ex.Message}", "ServerError"));
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

        private string GenerateOTP()
        {
            // Generate 6-digit OTP
            var random = new Random();
            return random.Next(100000, 999999).ToString();
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

    public class VerifyOTPRequest
    {
        public string Email { get; set; }
        public string OTP { get; set; }
    }

    public class ResetPasswordRequest
    {
        public string Email { get; set; }
        public string OTP { get; set; }
        public string NewPassword { get; set; }
    }

    #endregion
}
