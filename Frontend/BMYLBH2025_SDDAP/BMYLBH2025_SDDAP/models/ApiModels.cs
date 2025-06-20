using System;

namespace BMYLBH2025_SDDAP.Models
{
    // Generic API Response wrapper
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public string ErrorCode { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }

    public class ApiResponse : ApiResponse<object>
    {
    }

    // Authentication Request Models
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

    // Authentication Response Models
    public class LoginResponseData
    {
        public int UserID { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
        public bool ShowResendVerification { get; set; }
        public bool ShowForgotPassword { get; set; }
        public int AttemptCount { get; set; }
    }

    public class RegisterResponseData
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        public bool RequiresEmailVerification { get; set; }
        public bool ShowLoginOption { get; set; }
    }

    public class EmailResponseData
    {
        public string Email { get; set; }
        public string Action { get; set; }
        public bool OTPSent { get; set; }
        public bool IsVerified { get; set; }
        public bool ShowResendVerification { get; set; }
    }
} 