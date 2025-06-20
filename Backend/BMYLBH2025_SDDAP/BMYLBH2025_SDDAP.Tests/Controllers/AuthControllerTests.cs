using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Moq;
using BMYLBH2025_SDDAP.Controllers;
using BMYLBH2025_SDDAP.Models;
using BMYLBH2025_SDDAP.Services;

namespace BMYLBH2025_SDDAP.Tests.Controllers
{
    [TestClass]
    public class AuthControllerTests
    {
        private Mock<IUserRepository> _mockUserRepository;
        private Mock<IEmailService> _mockEmailService;
        private AuthController _controller;
        private User _testUser;

        [TestInitialize]
        public void Setup()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockEmailService = new Mock<IEmailService>();
            _controller = new AuthController(_mockUserRepository.Object, _mockEmailService.Object);
            
            // Setup test user
            _testUser = new User
            {
                UserID = 1,
                Username = "testuser",
                Email = "test@example.com",
                FullName = "Test User",
                PasswordHash = "$2a$11$abcdefghijklmnopqrstuvwxyz123456789",
                Role = "User",
                IsEmailVerified = true,
                EmailVerificationToken = null,
                CreatedAt = DateTime.Now
            };
        }

        [TestCleanup]
        public void Cleanup()
        {
            _controller?.Dispose();
        }

        #region POST /api/auth/login Tests

        [TestMethod]
        public void Login_WithValidCredentials_ShouldReturnSuccessResponse()
        {
            // Arrange
            var loginRequest = new LoginRequest
            {
                Email = "test@example.com",
                Password = "password123"
            };
            _mockUserRepository.Setup(r => r.GetByEmail("test@example.com")).Returns(_testUser);

            // Act
            var result = _controller.Login(loginRequest);

            // Assert
            result.Should().BeOfType<JsonResult<ApiResponse<LoginResponseData>>>();
            var jsonResult = result as JsonResult<ApiResponse<LoginResponseData>>;
            jsonResult.Content.Success.Should().BeTrue();
            jsonResult.Content.Message.Should().Be("Login successful");
            jsonResult.Content.Data.Email.Should().Be("test@example.com");
            jsonResult.Content.Data.Token.Should().NotBeNullOrEmpty();
        }

        [TestMethod]
        public void Login_WithNullRequest_ShouldReturnValidationError()
        {
            // Act
            var result = _controller.Login(null);

            // Assert
            result.Should().BeOfType<JsonResult<ApiResponse<LoginResponseData>>>();
            var jsonResult = result as JsonResult<ApiResponse<LoginResponseData>>;
            jsonResult.Content.Success.Should().BeFalse();
            jsonResult.Content.ErrorCode.Should().Be("ValidationError");
            jsonResult.Content.Message.Should().Be("Email and password are required.");
        }

        [TestMethod]
        public void Login_WithEmptyEmail_ShouldReturnValidationError()
        {
            // Arrange
            var loginRequest = new LoginRequest
            {
                Email = "",
                Password = "password123"
            };

            // Act
            var result = _controller.Login(loginRequest);

            // Assert
            result.Should().BeOfType<JsonResult<ApiResponse<LoginResponseData>>>();
            var jsonResult = result as JsonResult<ApiResponse<LoginResponseData>>;
            jsonResult.Content.Success.Should().BeFalse();
            jsonResult.Content.ErrorCode.Should().Be("ValidationError");
        }

        [TestMethod]
        public void Login_WithNonExistentUser_ShouldReturnInvalidCredentials()
        {
            // Arrange
            var loginRequest = new LoginRequest
            {
                Email = "nonexistent@example.com",
                Password = "password123"
            };
            _mockUserRepository.Setup(r => r.GetByEmail("nonexistent@example.com")).Returns((User)null);

            // Act
            var result = _controller.Login(loginRequest);

            // Assert
            result.Should().BeOfType<JsonResult<ApiResponse<LoginResponseData>>>();
            var jsonResult = result as JsonResult<ApiResponse<LoginResponseData>>;
            jsonResult.Content.Success.Should().BeFalse();
            jsonResult.Content.ErrorCode.Should().Be("InvalidCredentials");
            jsonResult.Content.Data.ShowForgotPassword.Should().BeTrue();
        }

        [TestMethod]
        public void Login_WithInvalidPassword_ShouldReturnInvalidPassword()
        {
            // Arrange
            var loginRequest = new LoginRequest
            {
                Email = "test@example.com",
                Password = "wrongpassword"
            };
            _mockUserRepository.Setup(r => r.GetByEmail("test@example.com")).Returns(_testUser);

            // Act
            var result = _controller.Login(loginRequest);

            // Assert
            result.Should().BeOfType<JsonResult<ApiResponse<LoginResponseData>>>();
            var jsonResult = result as JsonResult<ApiResponse<LoginResponseData>>;
            jsonResult.Content.Success.Should().BeFalse();
            jsonResult.Content.ErrorCode.Should().Be("InvalidPassword");
            jsonResult.Content.Data.ShowForgotPassword.Should().BeTrue();
        }

        [TestMethod]
        public void Login_WithUnverifiedEmail_ShouldReturnEmailNotVerified()
        {
            // Arrange
            var unverifiedUser = new User
            {
                UserID = 2,
                Email = "unverified@example.com",
                PasswordHash = "$2a$11$abcdefghijklmnopqrstuvwxyz123456789",
                IsEmailVerified = false
            };
            var loginRequest = new LoginRequest
            {
                Email = "unverified@example.com",
                Password = "password123"
            };
            _mockUserRepository.Setup(r => r.GetByEmail("unverified@example.com")).Returns(unverifiedUser);

            // Act
            var result = _controller.Login(loginRequest);

            // Assert
            result.Should().BeOfType<JsonResult<ApiResponse<LoginResponseData>>>();
            var jsonResult = result as JsonResult<ApiResponse<LoginResponseData>>;
            jsonResult.Content.Success.Should().BeFalse();
            jsonResult.Content.ErrorCode.Should().Be("EmailNotVerified");
            jsonResult.Content.Data.ShowResendVerification.Should().BeTrue();
        }

        [TestMethod]
        public void Login_WithRepositoryException_ShouldReturnServerError()
        {
            // Arrange
            var loginRequest = new LoginRequest
            {
                Email = "test@example.com",
                Password = "password123"
            };
            _mockUserRepository.Setup(r => r.GetByEmail("test@example.com")).Throws(new Exception("Database error"));

            // Act
            var result = _controller.Login(loginRequest);

            // Assert
            result.Should().BeOfType<JsonResult<ApiResponse<LoginResponseData>>>();
            var jsonResult = result as JsonResult<ApiResponse<LoginResponseData>>;
            jsonResult.Content.Success.Should().BeFalse();
            jsonResult.Content.ErrorCode.Should().Be("ServerError");
        }

        #endregion

        #region POST /api/auth/register Tests

        [TestMethod]
        public async Task Register_WithValidData_ShouldReturnSuccessResponse()
        {
            // Arrange
            var registerRequest = new RegisterRequest
            {
                Email = "newuser@example.com",
                Password = "password123",
                FullName = "New User"
            };
            _mockUserRepository.Setup(r => r.GetByEmail("newuser@example.com")).Returns((User)null);
            _mockUserRepository.Setup(r => r.Create(It.IsAny<User>()));
            _mockEmailService.Setup(s => s.SendVerificationEmailAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);
            _mockEmailService.Setup(s => s.SendWelcomeEmailAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Register(registerRequest);

            // Assert
            result.Should().BeOfType<JsonResult<ApiResponse<RegisterResponseData>>>();
            var jsonResult = result as JsonResult<ApiResponse<RegisterResponseData>>;
            jsonResult.Content.Success.Should().BeTrue();
            jsonResult.Content.Message.Should().Contain("Registration successful");
            jsonResult.Content.Data.Email.Should().Be("newuser@example.com");
            _mockUserRepository.Verify(r => r.Create(It.IsAny<User>()), Times.Once);
            _mockEmailService.Verify(s => s.SendVerificationEmailAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _mockEmailService.Verify(s => s.SendWelcomeEmailAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public async Task Register_WithNullRequest_ShouldReturnValidationError()
        {
            // Act
            var result = await _controller.Register(null);

            // Assert
            result.Should().BeOfType<JsonResult<ApiResponse<RegisterResponseData>>>();
            var jsonResult = result as JsonResult<ApiResponse<RegisterResponseData>>;
            jsonResult.Content.Success.Should().BeFalse();
            jsonResult.Content.ErrorCode.Should().Be("ValidationError");
            jsonResult.Content.Message.Should().Be("All fields are required.");
        }

        [TestMethod]
        public async Task Register_WithEmptyFields_ShouldReturnValidationError()
        {
            // Arrange
            var registerRequest = new RegisterRequest
            {
                Email = "",
                Password = "password123",
                FullName = "Test User"
            };

            // Act
            var result = await _controller.Register(registerRequest);

            // Assert
            result.Should().BeOfType<JsonResult<ApiResponse<RegisterResponseData>>>();
            var jsonResult = result as JsonResult<ApiResponse<RegisterResponseData>>;
            jsonResult.Content.Success.Should().BeFalse();
            jsonResult.Content.ErrorCode.Should().Be("ValidationError");
        }

        [TestMethod]
        public async Task Register_WithExistingEmail_ShouldReturnUserExistsError()
        {
            // Arrange
            var registerRequest = new RegisterRequest
            {
                Email = "test@example.com",
                Password = "password123",
                FullName = "Test User"
            };
            _mockUserRepository.Setup(r => r.GetByEmail("test@example.com")).Returns(_testUser);

            // Act
            var result = await _controller.Register(registerRequest);

            // Assert
            result.Should().BeOfType<JsonResult<ApiResponse<RegisterResponseData>>>();
            var jsonResult = result as JsonResult<ApiResponse<RegisterResponseData>>;
            jsonResult.Content.Success.Should().BeFalse();
            jsonResult.Content.ErrorCode.Should().Be("UserExists");
            jsonResult.Content.Message.Should().Be("User with this email already exists.");
        }

        [TestMethod]
        public async Task Register_WithEmailServiceFailure_ShouldStillReturnSuccess()
        {
            // Arrange
            var registerRequest = new RegisterRequest
            {
                Email = "newuser@example.com",
                Password = "password123",
                FullName = "New User"
            };
            _mockUserRepository.Setup(r => r.GetByEmail("newuser@example.com")).Returns((User)null);
            _mockUserRepository.Setup(r => r.Create(It.IsAny<User>()));
            _mockEmailService.Setup(s => s.SendVerificationEmailAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception("Email service error"));

            // Act
            var result = await _controller.Register(registerRequest);

            // Assert
            result.Should().BeOfType<JsonResult<ApiResponse<RegisterResponseData>>>();
            var jsonResult = result as JsonResult<ApiResponse<RegisterResponseData>>;
            jsonResult.Content.ErrorCode.Should().Be("ServerError");
        }

        #endregion

        #region GET /api/auth/verify-email Tests

        [TestMethod]
        public void VerifyEmail_WithValidToken_ShouldReturnSuccessResponse()
        {
            // Arrange
            var verificationToken = "valid-token-123";
            var unverifiedUser = new User
            {
                UserID = 2,
                Email = "test@example.com",
                IsEmailVerified = false,
                EmailVerificationToken = verificationToken
            };
            _mockUserRepository.Setup(r => r.GetByVerificationToken(verificationToken)).Returns(unverifiedUser);
            _mockUserRepository.Setup(r => r.Update(It.IsAny<User>()));

            // Act
            var result = _controller.VerifyEmail(verificationToken);

            // Assert
            result.Should().BeOfType<JsonResult<ApiResponse<EmailResponseData>>>();
            var jsonResult = result as JsonResult<ApiResponse<EmailResponseData>>;
            jsonResult.Content.Success.Should().BeTrue();
            jsonResult.Content.Message.Should().Be("Email verified successfully! You can now log in.");
            jsonResult.Content.Data.Email.Should().Be("test@example.com");
            _mockUserRepository.Verify(r => r.Update(It.IsAny<User>()), Times.Once);
        }

        [TestMethod]
        public void VerifyEmail_WithEmptyToken_ShouldReturnValidationError()
        {
            // Act
            var result = _controller.VerifyEmail("");

            // Assert
            result.Should().BeOfType<JsonResult<ApiResponse<EmailResponseData>>>();
            var jsonResult = result as JsonResult<ApiResponse<EmailResponseData>>;
            jsonResult.Content.Success.Should().BeFalse();
            jsonResult.Content.ErrorCode.Should().Be("ValidationError");
            jsonResult.Content.Message.Should().Be("Verification token is required.");
        }

        [TestMethod]
        public void VerifyEmail_WithInvalidToken_ShouldReturnInvalidTokenError()
        {
            // Arrange
            var invalidToken = "invalid-token";
            _mockUserRepository.Setup(r => r.GetByVerificationToken(invalidToken)).Returns((User)null);

            // Act
            var result = _controller.VerifyEmail(invalidToken);

            // Assert
            result.Should().BeOfType<JsonResult<ApiResponse<EmailResponseData>>>();
            var jsonResult = result as JsonResult<ApiResponse<EmailResponseData>>;
            jsonResult.Content.Success.Should().BeFalse();
            jsonResult.Content.ErrorCode.Should().Be("InvalidToken");
            jsonResult.Content.Message.Should().Be("Invalid verification token.");
        }

        [TestMethod]
        public void VerifyEmail_WithAlreadyVerifiedUser_ShouldReturnAlreadyVerified()
        {
            // Arrange
            var verificationToken = "token-123";
            var verifiedUser = new User
            {
                UserID = 1,
                Email = "test@example.com",
                IsEmailVerified = true,
                EmailVerificationToken = verificationToken
            };
            _mockUserRepository.Setup(r => r.GetByVerificationToken(verificationToken)).Returns(verifiedUser);

            // Act
            var result = _controller.VerifyEmail(verificationToken);

            // Assert
            result.Should().BeOfType<JsonResult<ApiResponse<EmailResponseData>>>();
            var jsonResult = result as JsonResult<ApiResponse<EmailResponseData>>;
            jsonResult.Content.Success.Should().BeTrue();
            jsonResult.Content.Message.Should().Be("Email is already verified.");
        }

        #endregion

        #region POST /api/auth/resend-verification Tests

        [TestMethod]
        public async Task ResendVerificationEmail_WithValidEmail_ShouldReturnSuccessResponse()
        {
            // Arrange
            var request = new ResendVerificationRequest { Email = "test@example.com" };
            var unverifiedUser = new User
            {
                UserID = 2,
                Email = "test@example.com",
                IsEmailVerified = false,
                EmailVerificationToken = "token-123"
            };
            _mockUserRepository.Setup(r => r.GetByEmail("test@example.com")).Returns(unverifiedUser);
            _mockEmailService.Setup(s => s.SendVerificationEmailAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.ResendVerificationEmail(request);

            // Assert
            result.Should().BeOfType<JsonResult<ApiResponse<EmailResponseData>>>();
            var jsonResult = result as JsonResult<ApiResponse<EmailResponseData>>;
            jsonResult.Content.Success.Should().BeTrue();
            jsonResult.Content.Message.Should().Be("Verification email sent successfully.");
            _mockEmailService.Verify(s => s.SendVerificationEmailAsync("test@example.com", "token-123"), Times.Once);
        }

        [TestMethod]
        public async Task ResendVerificationEmail_WithNonExistentUser_ShouldReturnUserNotFound()
        {
            // Arrange
            var request = new ResendVerificationRequest { Email = "nonexistent@example.com" };
            _mockUserRepository.Setup(r => r.GetByEmail("nonexistent@example.com")).Returns((User)null);

            // Act
            var result = await _controller.ResendVerificationEmail(request);

            // Assert
            result.Should().BeOfType<JsonResult<ApiResponse<EmailResponseData>>>();
            var jsonResult = result as JsonResult<ApiResponse<EmailResponseData>>;
            jsonResult.Content.Success.Should().BeFalse();
            jsonResult.Content.ErrorCode.Should().Be("UserNotFound");
            jsonResult.Content.Message.Should().Be("User not found.");
        }

        [TestMethod]
        public async Task ResendVerificationEmail_WithAlreadyVerifiedUser_ShouldReturnAlreadyVerified()
        {
            // Arrange
            var request = new ResendVerificationRequest { Email = "test@example.com" };
            _mockUserRepository.Setup(r => r.GetByEmail("test@example.com")).Returns(_testUser);

            // Act
            var result = await _controller.ResendVerificationEmail(request);

            // Assert
            result.Should().BeOfType<JsonResult<ApiResponse<EmailResponseData>>>();
            var jsonResult = result as JsonResult<ApiResponse<EmailResponseData>>;
            jsonResult.Content.Success.Should().BeTrue();
            jsonResult.Content.Message.Should().Be("Email is already verified.");
        }

        #endregion

        #region POST /api/auth/forgot-password Tests

        [TestMethod]
        public async Task ForgotPassword_WithValidEmail_ShouldReturnSuccessResponse()
        {
            // Arrange
            var request = new ForgotPasswordRequest { Email = "test@example.com" };
            _mockUserRepository.Setup(r => r.GetByEmail("test@example.com")).Returns(_testUser);
            _mockUserRepository.Setup(r => r.Update(It.IsAny<User>()));
            _mockEmailService.Setup(s => s.SendPasswordResetEmailAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.ForgotPassword(request);

            // Assert
            result.Should().BeOfType<JsonResult<ApiResponse<EmailResponseData>>>();
            var jsonResult = result as JsonResult<ApiResponse<EmailResponseData>>;
            jsonResult.Content.Success.Should().BeTrue();
            jsonResult.Content.Message.Should().Be("Password reset instructions sent to your email.");
            _mockUserRepository.Verify(r => r.Update(It.IsAny<User>()), Times.Once);
            _mockEmailService.Verify(s => s.SendPasswordResetEmailAsync("test@example.com", It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public async Task ForgotPassword_WithNonExistentUser_ShouldReturnUserNotFound()
        {
            // Arrange
            var request = new ForgotPasswordRequest { Email = "nonexistent@example.com" };
            _mockUserRepository.Setup(r => r.GetByEmail("nonexistent@example.com")).Returns((User)null);

            // Act
            var result = await _controller.ForgotPassword(request);

            // Assert
            result.Should().BeOfType<JsonResult<ApiResponse<EmailResponseData>>>();
            var jsonResult = result as JsonResult<ApiResponse<EmailResponseData>>;
            jsonResult.Content.Success.Should().BeFalse();
            jsonResult.Content.ErrorCode.Should().Be("UserNotFound");
        }

        #endregion

        #region POST /api/auth/verify-reset-otp Tests

        [TestMethod]
        public void VerifyResetOTP_WithValidOTP_ShouldReturnSuccessResponse()
        {
            // Arrange
            var request = new VerifyOTPRequest { Email = "test@example.com", OTP = "123456" };
            var userWithOTP = new User
            {
                UserID = 1,
                Email = "test@example.com",
                PasswordResetOTP = "123456",
                PasswordResetOTPExpiry = DateTime.Now.AddMinutes(10)
            };
            _mockUserRepository.Setup(r => r.GetByEmail("test@example.com")).Returns(userWithOTP);

            // Act
            var result = _controller.VerifyResetOTP(request);

            // Assert
            result.Should().BeOfType<JsonResult<ApiResponse<EmailResponseData>>>();
            var jsonResult = result as JsonResult<ApiResponse<EmailResponseData>>;
            jsonResult.Content.Success.Should().BeTrue();
            jsonResult.Content.Message.Should().Be("OTP verified successfully. You can now reset your password.");
        }

        [TestMethod]
        public void VerifyResetOTP_WithInvalidOTP_ShouldReturnInvalidOTP()
        {
            // Arrange
            var request = new VerifyOTPRequest { Email = "test@example.com", OTP = "wrong" };
            var userWithOTP = new User
            {
                UserID = 1,
                Email = "test@example.com",
                PasswordResetOTP = "123456",
                PasswordResetOTPExpiry = DateTime.Now.AddMinutes(10)
            };
            _mockUserRepository.Setup(r => r.GetByEmail("test@example.com")).Returns(userWithOTP);

            // Act
            var result = _controller.VerifyResetOTP(request);

            // Assert
            result.Should().BeOfType<JsonResult<ApiResponse<EmailResponseData>>>();
            var jsonResult = result as JsonResult<ApiResponse<EmailResponseData>>;
            jsonResult.Content.Success.Should().BeFalse();
            jsonResult.Content.ErrorCode.Should().Be("InvalidOTP");
        }

        [TestMethod]
        public void VerifyResetOTP_WithExpiredOTP_ShouldReturnExpiredOTP()
        {
            // Arrange
            var request = new VerifyOTPRequest { Email = "test@example.com", OTP = "123456" };
            var userWithExpiredOTP = new User
            {
                UserID = 1,
                Email = "test@example.com",
                PasswordResetOTP = "123456",
                PasswordResetOTPExpiry = DateTime.Now.AddMinutes(-10) // Expired
            };
            _mockUserRepository.Setup(r => r.GetByEmail("test@example.com")).Returns(userWithExpiredOTP);

            // Act
            var result = _controller.VerifyResetOTP(request);

            // Assert
            result.Should().BeOfType<JsonResult<ApiResponse<EmailResponseData>>>();
            var jsonResult = result as JsonResult<ApiResponse<EmailResponseData>>;
            jsonResult.Content.Success.Should().BeFalse();
            jsonResult.Content.ErrorCode.Should().Be("ExpiredOTP");
        }

        #endregion

        #region POST /api/auth/reset-password Tests

        [TestMethod]
        public void ResetPassword_WithValidData_ShouldReturnSuccessResponse()
        {
            // Arrange
            var request = new ResetPasswordRequest 
            { 
                Email = "test@example.com", 
                OTP = "123456", 
                NewPassword = "newpassword123" 
            };
            var userWithValidOTP = new User
            {
                UserID = 1,
                Email = "test@example.com",
                PasswordResetOTP = "123456",
                PasswordResetOTPExpiry = DateTime.Now.AddMinutes(10)
            };
            _mockUserRepository.Setup(r => r.GetByEmail("test@example.com")).Returns(userWithValidOTP);
            _mockUserRepository.Setup(r => r.Update(It.IsAny<User>()));

            // Act
            var result = _controller.ResetPassword(request);

            // Assert
            result.Should().BeOfType<JsonResult<ApiResponse<EmailResponseData>>>();
            var jsonResult = result as JsonResult<ApiResponse<EmailResponseData>>;
            jsonResult.Content.Success.Should().BeTrue();
            jsonResult.Content.Message.Should().Be("Password reset successfully. You can now log in with your new password.");
            _mockUserRepository.Verify(r => r.Update(It.IsAny<User>()), Times.Once);
        }

        [TestMethod]
        public void ResetPassword_WithInvalidOTP_ShouldReturnInvalidOTP()
        {
            // Arrange
            var request = new ResetPasswordRequest 
            { 
                Email = "test@example.com", 
                OTP = "wrong", 
                NewPassword = "newpassword123" 
            };
            var userWithOTP = new User
            {
                UserID = 1,
                Email = "test@example.com",
                PasswordResetOTP = "123456",
                PasswordResetOTPExpiry = DateTime.Now.AddMinutes(10)
            };
            _mockUserRepository.Setup(r => r.GetByEmail("test@example.com")).Returns(userWithOTP);

            // Act
            var result = _controller.ResetPassword(request);

            // Assert
            result.Should().BeOfType<JsonResult<ApiResponse<EmailResponseData>>>();
            var jsonResult = result as JsonResult<ApiResponse<EmailResponseData>>;
            jsonResult.Content.Success.Should().BeFalse();
            jsonResult.Content.ErrorCode.Should().Be("InvalidOTP");
        }

        #endregion

        #region Performance Tests

        [TestMethod]
        public void Login_ShouldCompleteWithinTimeLimit()
        {
            // Arrange
            var loginRequest = new LoginRequest
            {
                Email = "test@example.com",
                Password = "password123"
            };
            _mockUserRepository.Setup(r => r.GetByEmail("test@example.com")).Returns(_testUser);
            var startTime = DateTime.Now;

            // Act
            var result = _controller.Login(loginRequest);

            // Assert
            var executionTime = DateTime.Now - startTime;
            executionTime.Should().BeLessThan(TimeSpan.FromSeconds(2));
            result.Should().BeOfType<JsonResult<ApiResponse<LoginResponseData>>>();
        }

        [TestMethod]
        public async Task Register_ShouldCompleteWithinTimeLimit()
        {
            // Arrange
            var registerRequest = new RegisterRequest
            {
                Email = "newuser@example.com",
                Password = "password123",
                FullName = "New User"
            };
            _mockUserRepository.Setup(r => r.GetByEmail("newuser@example.com")).Returns((User)null);
            _mockUserRepository.Setup(r => r.Create(It.IsAny<User>()));
            _mockEmailService.Setup(s => s.SendVerificationEmailAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);
            _mockEmailService.Setup(s => s.SendWelcomeEmailAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);
            var startTime = DateTime.Now;

            // Act
            var result = await _controller.Register(registerRequest);

            // Assert
            var executionTime = DateTime.Now - startTime;
            executionTime.Should().BeLessThan(TimeSpan.FromSeconds(3));
            result.Should().BeOfType<JsonResult<ApiResponse<RegisterResponseData>>>();
        }

        #endregion
    }
} 