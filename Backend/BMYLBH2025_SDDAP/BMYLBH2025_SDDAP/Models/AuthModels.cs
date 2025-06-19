namespace BMYLBH2025_SDDAP.Models
{
    /// <summary>
    /// Login response data
    /// </summary>
    public class LoginResponseData
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
        public bool ShowResendVerification { get; set; }
        public bool ShowForgotPassword { get; set; }
    }

    /// <summary>
    /// Registration response data
    /// </summary>
    public class RegisterResponseData
    {
        public int UserId { get; set; }
        public string Email { get; set; }
    }

    /// <summary>
    /// Email operation response data
    /// </summary>
    public class EmailResponseData
    {
        public string Email { get; set; }
        public string Action { get; set; } // "verification", "reset", etc.
    }

    /// <summary>
    /// OTP verification response data
    /// </summary>
    public class OTPResponseData
    {
        public string Email { get; set; }
        public bool IsValid { get; set; }
    }

    /// <summary>
    /// Password reset response data
    /// </summary>
    public class PasswordResetResponseData
    {
        public string Email { get; set; }
        public bool Success { get; set; }
    }
} 